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
                // Controlla se l'utente è un admin o un utente normale
                var isAdmin = dbContext.Admin.Any(a => a.Nome == model.Nome && a.Password == model.Password);

                if (isAdmin)
                {
                    // Se è un admin, imposta IsAdmin a true nel modello di login
                    model.IsAdmin = true;
                }
                else
                {
                    // Se non è un admin, imposta IsAdmin a false nel modello di login
                    model.IsAdmin = false;
                }

                // Effettua il login
                if (isAdmin || dbContext.Utenti.Any(u => u.Email == model.Nome && u.Password == model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.Nome, true);
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
