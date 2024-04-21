using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;
using Travel_Nest.Models;

namespace Travel_Nest.Controllers
{
    public class PrenotazioniController : Controller
    {
        private readonly TravelDb db = new TravelDb();

        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Login");
            }
            var userData = ((FormsIdentity)User.Identity).Ticket.UserData;
            if (!int.TryParse(userData, out int userId))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var prenotazioni = db.Prenotazioni.Where(p => p.IDUtente == userId).ToList();

            return View(prenotazioni);
        }


        public ActionResult Dettagli(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Prenotazioni prenotazione = db.Prenotazioni.Find(id);
            if (prenotazione == null)
            {
                return HttpNotFound();
            }

            return View(prenotazione);
        }

        public ActionResult NuovaPrenotazione(int idAlloggio)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Login");
            }

            var userData = ((FormsIdentity)User.Identity).Ticket.UserData;
            if (!int.TryParse(userData, out int userId))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var nuovaPrenotazione = new Prenotazioni { IDUtente = userId, IDAlloggio = idAlloggio };

            return View(nuovaPrenotazione);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> NuovaPrenotazione([Bind(Include = "IDPrenotazione,IDUtente,IDAlloggio,DataCheckIn,DataCheckOut,StatoPrenotazione")] Prenotazioni prenotazione)
        {
            if (ModelState.IsValid)
            {
                prenotazione.StatoPrenotazione = "Confermato";
                db.Prenotazioni.Add(prenotazione);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.IDUtente = new SelectList(db.Utenti, "IDUtente", "Nome", prenotazione.IDUtente);
            ViewBag.IDAlloggio = new SelectList(db.Alloggi, "IDAlloggio", "NomeAlloggio", prenotazione.IDAlloggio);
            return View(prenotazione);
        }

        public ActionResult Modifica(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Prenotazioni prenotazione = db.Prenotazioni.Find(id);
            if (prenotazione == null)
            {
                return HttpNotFound();
            }

            ViewBag.IDUtente = new SelectList(db.Utenti, "IDUtente", "Nome", prenotazione.IDUtente);
            ViewBag.IDAlloggio = new SelectList(db.Alloggi, "IDAlloggio", "NomeAlloggio", prenotazione.IDAlloggio);
            return View(prenotazione);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Modifica([Bind(Include = "IDPrenotazione,IDUtente,IDAlloggio,DataCheckIn,DataCheckOut,StatoPrenotazione")] Prenotazioni prenotazione)
        {
            if (ModelState.IsValid)
            {
                db.Entry(prenotazione).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.IDUtente = new SelectList(db.Utenti, "IDUtente", "Nome", prenotazione.IDUtente);
            ViewBag.IDAlloggio = new SelectList(db.Alloggi, "IDAlloggio", "NomeAlloggio", prenotazione.IDAlloggio);
            return View(prenotazione);
        }

        public ActionResult ConfermaEliminazione(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Prenotazioni prenotazione = db.Prenotazioni.Find(id);
            if (prenotazione == null)
            {
                return HttpNotFound();
            }

            return View(prenotazione);
        }

        [HttpPost,]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Elimina(int id)
        {
            Prenotazioni prenotazione = await db.Prenotazioni.FindAsync(id);
            db.Prenotazioni.Remove(prenotazione);
            await db.SaveChangesAsync();
            TempData["PrenotazioneCancellata"] = "Prenotazione Cancellata";
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
    }
}
