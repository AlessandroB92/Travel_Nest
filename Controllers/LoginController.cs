using System.Linq;
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
                var isAdmin = dbContext.Admin.Any(a => a.Nome == User.Identity.Name);
                if (isAdmin)
                {
                    Roles.AddUserToRole(User.Identity.Name, "Admin");
                }
                else
                {
                    Roles.AddUserToRole(User.Identity.Name, "User");
                }

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
                var user = dbContext.Utenti.FirstOrDefault(u => u.Nome == model.Nome && u.Password == model.Password);
                var admin = dbContext.Admin.FirstOrDefault(a => a.Nome == model.Nome && a.Password == model.Password);
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(user.Nome, true);
                    return RedirectToAction("Index", "Home");
                }
                if (admin != null)
                {
                    FormsAuthentication.SetAuthCookie(admin.Nome, true);
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
