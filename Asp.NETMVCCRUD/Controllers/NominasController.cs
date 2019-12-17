using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Asp.NETMVCCRUD.Models;

namespace Asp.NETMVCCRUD.Controllers
{
    public class NominasController : Controller
    {
        private MvcCRUDDBEntities db = new MvcCRUDDBEntities();

        // GET: Nominas
        public ActionResult Index()
        {
            if(Session["Empleado"]!=null)
            {
                var empleado = (int)Session["Empleado"];
                var nominas = db.Nominas.Include(n => n.Empleados).Where(x => x.EmpleadoID == empleado);
                return View(nominas.ToList());
            }
            
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Login");
        }

        // GET: Nominas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nominas nominas = db.Nominas.Find(id);
            if (nominas == null)
            {
                return HttpNotFound();
            }
            return View(nominas);
        }

        // GET: Nominas/Create
        public ActionResult Create()
        {
            ViewBag.EmpleadoID = new SelectList(db.Empleados, "EmpleadoID", "Nombre");
            return View();
        }

        // POST: Nominas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NominaID,EmpleadoID,IngresoBase,DeduccionDesayuno,DeduccionAhorro,FechaNomina")] Nominas nominas)
        {
            if (ModelState.IsValid)
            {
                db.Nominas.Add(nominas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmpleadoID = new SelectList(db.Empleados, "EmpleadoID", "Nombre", nominas.EmpleadoID);
            return View(nominas);
        }

        // GET: Nominas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nominas nominas = db.Nominas.Find(id);
            if (nominas == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmpleadoID = new SelectList(db.Empleados, "EmpleadoID", "Nombre", nominas.EmpleadoID);
            return View(nominas);
        }

        // POST: Nominas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NominaID,EmpleadoID,IngresoBase,DeduccionDesayuno,DeduccionAhorro,FechaNomina")] Nominas nominas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nominas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmpleadoID = new SelectList(db.Empleados, "EmpleadoID", "Nombre", nominas.EmpleadoID);
            return View(nominas);
        }

        // GET: Nominas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nominas nominas = db.Nominas.Find(id);
            if (nominas == null)
            {
                return HttpNotFound();
            }
            return View(nominas);
        }

        // POST: Nominas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Nominas nominas = db.Nominas.Find(id);
            db.Nominas.Remove(nominas);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        public ActionResult GenearNomina()
        {
            var nominas = db.sp_CrearNomina();
            return RedirectToAction("Index");
        }


    }
}
