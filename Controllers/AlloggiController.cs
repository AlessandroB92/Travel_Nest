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
using System.IO;

namespace Travel_Nest.Controllers
{
    public class AlloggiController : Controller
    {
        private readonly TravelDb db = new TravelDb();

        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Index()
        {
            var alloggi = db.Alloggi.Include(a => a.Utenti);
            return View(await alloggi.ToListAsync());
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.IDUtente = new SelectList(db.Utenti, "IDUtente", "Nome");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Create([Bind(Include = "IDAlloggio,NomeAlloggio,Descrizione,Citta,PrezzoPerNotte,Disponibilita")] Alloggi alloggi)
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
        [Authorize(Roles = "Admin")]
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit([Bind(Include = "IDAlloggio,NomeAlloggio,Descrizione,Citta,PrezzoPerNotte,Disponibilita")] Alloggi alloggi)
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
        [Authorize(Roles = "Admin")]
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

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public ActionResult CaricaImmagine(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Alloggi alloggio = db.Alloggi.Find(id);
            if (alloggio == null)
            {
                return HttpNotFound();
            }

            return View(alloggio);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult CaricaImmagine(int id, HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                byte[] imageData;
                using (var binaryReader = new BinaryReader(file.InputStream))
                {
                    imageData = binaryReader.ReadBytes(file.ContentLength);
                }

                db.ImmaginiAlloggi.Add(new ImmaginiAlloggi
                {
                    IDAlloggio = id,
                    URLImmagine = file.FileName,
                    FileData = imageData
                });
                db.SaveChanges();
            }

            return RedirectToAction("Index", new { id });
        }

        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> EliminaImmagine(int? idAlloggio, int? idImmagine)
        {
            if (idAlloggio == null || idImmagine == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ImmaginiAlloggi immagine = await db.ImmaginiAlloggi.FindAsync(idImmagine);
            if (immagine == null)
            {
                return HttpNotFound();
            }

            db.ImmaginiAlloggi.Remove(immagine);
            await db.SaveChangesAsync();

            return RedirectToAction("Edit", new { id = idAlloggio });
        }


    }
}
