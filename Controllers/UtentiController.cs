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
    public class UtentiController : Controller
    {
        private TravelDb db = new TravelDb();

        // GET: Utenti
        public async Task<ActionResult> Index()
        {
            return View(await db.Utenti.ToListAsync());
        }

        // GET: Utenti/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Utenti utenti = await db.Utenti.FindAsync(id);
            if (utenti == null)
            {
                return HttpNotFound();
            }
            return View(utenti);
        }

        // GET: Utenti/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Utenti/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IDUtente,Nome,Cognome,Email,Password,Ruolo")] Utenti utenti)
        {
            if (ModelState.IsValid)
            {
                db.Utenti.Add(utenti);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(utenti);
        }

        // GET: Utenti/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Utenti utenti = await db.Utenti.FindAsync(id);
            if (utenti == null)
            {
                return HttpNotFound();
            }
            return View(utenti);
        }

        // POST: Utenti/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IDUtente,Nome,Cognome,Email,Password,Ruolo")] Utenti utenti)
        {
            if (ModelState.IsValid)
            {
                db.Entry(utenti).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(utenti);
        }

        // GET: Utenti/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Utenti utenti = await db.Utenti.FindAsync(id);
            if (utenti == null)
            {
                return HttpNotFound();
            }
            return View(utenti);
        }

        // POST: Utenti/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Utenti utenti = await db.Utenti.FindAsync(id);
            db.Utenti.Remove(utenti);
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
