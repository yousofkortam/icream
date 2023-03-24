using icream.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace icream.Controllers
{
    public class galleryController : Controller
    {
        icreamContext db;
        public IActionResult Index()
        {
            db = new icreamContext();
            var gallery = db.Galleries.Include(n => n.category).ToList();
            var category = db.Categories.ToList();
            ViewBag.Category = category;
            return View(gallery);
        }
    }
}
