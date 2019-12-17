using Asp.NETMVCCRUD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Asp.NETMVCCRUD.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private MvcCRUDDBEntities db = new MvcCRUDDBEntities();

        // GET: Dashboard
        public ActionResult Index(string message = "")
        {
            if(Session["Rol"]!=null)
            {
                if ((int)Session["Rol"] == 1)
                {
                    ViewBag.Message = message;
                    return View();
                }
                return RedirectToAction("Index", "Nominas");
            }

            FormsAuthentication.SignOut();
            return RedirectToAction("Index","Login");
        }

        public ActionResult GenerarNomina()
        {
            var nominas = db.sp_CrearNomina();
            return RedirectToAction("Index", new { message = "La nomina se genero correctamente" });
        }
    }
}