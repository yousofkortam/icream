using icream.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace icream.Controllers
{
    public class orderController : Controller
    {
        icreamContext db;
        public IActionResult Index()
        {
            int? userId = HttpContext.Session.GetInt32("userid");
            if (userId == null)
            {
                return RedirectToAction("login", "user");
            }
            db = new icreamContext();
            var carts = db.Carts.Where(p => p.user_id == userId).Include(n => n.product).ToList();
            ViewBag.user = db.Users.Where(u => u.id == userId).SingleOrDefault();
            return View(carts);
        }
    }
}
