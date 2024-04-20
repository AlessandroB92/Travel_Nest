using System.Linq;
using System.Web.Mvc;
using Travel_Nest.Models;

public class AdminController : Controller
{
    private readonly TravelDb _context;

    public AdminController()
    {
        _context = new TravelDb(); // Assicurati di avere un costruttore nel tuo DbContext
    }

    public ActionResult Index()
    {
        var recensioni = _context.Recensioni.ToList();
        return View(recensioni);
    }

    [HttpPost]
    public ActionResult DeleteRecensione(int id)
    {
        var recensione = _context.Recensioni.Find(id);
        if (recensione == null)
        {
            return HttpNotFound();
        }

        _context.Recensioni.Remove(recensione);
        _context.SaveChanges();

        return RedirectToAction("Index");
    }
}
