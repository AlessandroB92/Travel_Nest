using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Travel_Nest.Models;

namespace back_end_s7.Controllers
{
    public class LoginController : Controller
    {
        private readonly TravelDb dbContext = new TravelDb();

        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.ErrorMessage = TempData["ErrorMessage"];
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                if (dbContext.Admin.Any(a => a.Nome == model.Nome && a.Password == model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.Nome, true);
                    var adminId = dbContext.Admin.FirstOrDefault(a => a.Nome == model.Nome)?.IDAdmin;
                    var userData = adminId.ToString();
                    var ticket = new FormsAuthenticationTicket(1, model.Nome, DateTime.Now, DateTime.Now.AddMinutes(30), true, userData);
                    var encryptedTicket = FormsAuthentication.Encrypt(ticket);
                    var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                    Response.Cookies.Add(authCookie);
                    return RedirectToAction("Index", "Home");
                }
                else if (dbContext.Utenti.Any(u => u.Email == model.Nome && u.Password == model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.Nome, true);
                    var userId = dbContext.Utenti.FirstOrDefault(u => u.Email == model.Nome).IDUtente;
                    var userData = userId.ToString();
                    var ticket = new FormsAuthenticationTicket(1, model.Nome, DateTime.Now, DateTime.Now.AddMinutes(30), true, userData);
                    var encryptedTicket = FormsAuthentication.Encrypt(ticket);
                    var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                    Response.Cookies.Add(authCookie);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["ErrorMessage"] = "Credenziali Errate.";
                    return RedirectToAction("Index", "Login");
                }
            }
            return View(model);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            TempData["Message"] = "Logout effettuato con successo.";
            return RedirectToAction("Index", "Login");
        }
    }
}
