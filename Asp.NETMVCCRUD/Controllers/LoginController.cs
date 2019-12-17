using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;
using System.Web.Security;
using Asp.NETMVCCRUD.Models;

namespace Asp.NETMVCCRUD.Controllers
{
    public class LoginController : Controller
    {
        // GET: api/Login
        public ActionResult Index(string message="")
        {
            if(User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Dashboard");
            }

            ViewBag.Message = message;
            return View();
        }

        [HttpPost]
        public ActionResult Login(string email, string password)
        {

            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(email))
            {
                MvcCRUDDBEntities db = new MvcCRUDDBEntities();

                var user = db.Empleados.FirstOrDefault(x => x.Email == email && x.Password == password);

                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(user.Email, true);
                    Session["Rol"] = user.Role;
                    Session["Empleado"] = user.EmpleadoID;
                    
                   return RedirectToAction("Index","Dashboard");
                }
                else
                {
                    return RedirectToAction("Index",new { message= "Usuario o contraseña incorrecta" });
                }

            }
           
           return RedirectToAction("Index", new { message = "Usuario o contraseña incorrecta" });
           
        }

        [Authorize]
        public ActionResult Logout()
        {

            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }


    }
}
