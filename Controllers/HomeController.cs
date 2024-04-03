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
            var alloggiWithImages = db.Alloggi.ToList().Select(a => new AlloggioCard
            {
                Alloggio = a,
                Immagini = a.Immagini.ToList()
            }).ToList();

            return View(alloggiWithImages);
        }
    }
}
