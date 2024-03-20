using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Travel_Nest.Models;

namespace Travel_Nest.Controllers
{
    public class AlloggiController : Controller
    {
        private TravelDb db = new TravelDb();

        // GET: Alloggi
        public async Task<ActionResult> Index()
        {
            var alloggi = db.Alloggi.Include(a => a.Utenti);
            return View(await alloggi.ToListAsync());
        }

        // GET: Alloggi/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Alloggi alloggi = await db.Alloggi.FindAsync(id);
            if (alloggi == null)
            {
                return HttpNotFound();
            }
            return View(alloggi);
        }

        // GET: Alloggi/Create
        public ActionResult Create()
        {
            ViewBag.IDUtente = new SelectList(db.Utenti, "IDUtente", "Nome");
            return View();
        }

        // POST: Alloggi/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IDAlloggio,IDUtente,NomeAlloggio,Descrizione,Citta,PrezzoPerNotte,Disponibilita")] Alloggi alloggi)
        {
            if (ModelState.IsValid)
            {
                db.Alloggi.Add(alloggi);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.IDUtente = new SelectList(db.Utenti, "IDUtente", "Nome", alloggi.IDUtente);
            return View(alloggi);
        }

        // GET: Alloggi/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Alloggi alloggi = await db.Alloggi.FindAsync(id);
            if (alloggi == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDUtente = new SelectList(db.Utenti, "IDUtente", "Nome", alloggi.IDUtente);
            return View(alloggi);
        }

        // POST: Alloggi/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IDAlloggio,IDUtente,NomeAlloggio,Descrizione,Citta,PrezzoPerNotte,Disponibilita")] Alloggi alloggi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(alloggi).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.IDUtente = new SelectList(db.Utenti, "IDUtente", "Nome", alloggi.IDUtente);
            return View(alloggi);
        }

        // GET: Alloggi/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Alloggi alloggi = await db.Alloggi.FindAsync(id);
            if (alloggi == null)
            {
                return HttpNotFound();
            }
            return View(alloggi);
        }

        // POST: Alloggi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Alloggi alloggi = await db.Alloggi.FindAsync(id);
            db.Alloggi.Remove(alloggi);
            await db.SaveChangesAsync();
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
