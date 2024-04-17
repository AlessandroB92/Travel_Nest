using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;
using Travel_Nest.Models;

namespace Travel_Nest.Controllers
{
    public class RecensioniController : Controller
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

            var recensioni = db.Recensioni
                .Include(r => r.Alloggi)
                .Where(r => r.IDUtente == userId)
                .ToList();

            return View(recensioni);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Recensioni recensione = db.Recensioni.Find(id);
            if (recensione == null)
            {
                return HttpNotFound();
            }

            return View(recensione);
        }

        public ActionResult Create(int idAlloggio)
        {
            var userData = ((FormsIdentity)User.Identity).Ticket.UserData;
            if (!int.TryParse(userData, out int userId))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var alloggio = db.Alloggi.Find(idAlloggio);
            if (alloggio == null)
            {
                return HttpNotFound();
            }

            var nuovaRecensione = new Recensioni
            {
                IDUtente = userId,
                IDAlloggio = idAlloggio
            };

            return View(nuovaRecensione);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(int idAlloggio, [Bind(Include = "IDRecensione,IDUtente,IDAlloggio,TestoRecensione,Valutazione,DataRecensione")] Recensioni recensione)
        {
            if (ModelState.IsValid)
            {
                var userData = ((FormsIdentity)User.Identity).Ticket.UserData;
                int userId;
                if (!int.TryParse(userData, out userId))
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                recensione.IDAlloggio = idAlloggio;
                recensione.IDUtente = userId;

                recensione.DataRecensione = DateTime.Now;

                db.Recensioni.Add(recensione);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(recensione);
        }

        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Recensioni recensione = await db.Recensioni.FindAsync(id);
            if (recensione == null)
            {
                return HttpNotFound();
            }

            return View(recensione);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IDRecensione,IDUtente,IDAlloggio,TestoRecensione,Valutazione,DataRecensione")] Recensioni recensione)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Verifica che l'ID dell'entità sia valido
                    if (recensione.IDRecensione == 0)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }

                    // Recupera l'entità dal database
                    var existingRecensione = await db.Recensioni.FindAsync(recensione.IDRecensione);
                    if (existingRecensione == null)
                    {
                        return HttpNotFound();
                    }

                    // Aggiorna l'entità con i nuovi valori
                    existingRecensione.TestoRecensione = recensione.TestoRecensione;
                    existingRecensione.Valutazione = recensione.Valutazione;

                    // Cambia lo stato dell'entità a Modified e salva le modifiche
                    db.Entry(existingRecensione).State = EntityState.Modified;
                    await db.SaveChangesAsync();

                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                // Gestione degli errori
                ModelState.AddModelError("", "Si è verificato un errore durante il salvataggio della recensione. Si prega di riprovare.");
                // Log dell'errore
                Console.WriteLine(ex.Message);
            }

            return View(recensione);
        }



        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Recensioni recensione = db.Recensioni.Find(id);
            if (recensione == null)
            {
                return HttpNotFound();
            }

            return View(recensione);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Recensioni recensione = await db.Recensioni.FindAsync(id);
            if (recensione == null)
            {
                return HttpNotFound();
            }

            db.Recensioni.Remove(recensione);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

    }
}
