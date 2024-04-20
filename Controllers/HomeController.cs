using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Travel_Nest.Models;

namespace Travel_Nest.Controllers
{
    public class HomeController : Controller
    {
        private readonly TravelDb db = new TravelDb();

        public ActionResult Index()
        {
            var alloggi = db.Alloggi.ToList().Select(a => new AlloggioCard
            {
                Alloggio = a,
                Immagini = a.Immagini.ToList()
            }).ToList();

            var recensioni = db.Recensioni.ToList();

            // Passa sia gli alloggi che le recensioni alla vista
            ViewBag.Alloggi = alloggi;
            ViewBag.Recensioni = recensioni;

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
