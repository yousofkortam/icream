using icream.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace icream.Controllers
{
    public class cartController : Controller
    {
        icreamContext db;
        public IActionResult Index()
        {
            db = new icreamContext();
            int? userid = HttpContext.Session.GetInt32("userid");
            if (userid == null)
            {
                return RedirectToAction("login", "user");
            }
            List<Cart> productsInCart = db.Carts.Where(n => n.user_id == userid).Include(n => n.product).ToList();
            return View(productsInCart);
        }

        public IActionResult remove(int id)
        {
            int? userid = HttpContext.Session.GetInt32("userid");
            if (userid == null)
            {
                return RedirectToAction("login", "user");
            }
            db = new icreamContext();
            var data = db.Carts.Where(n => n.id == id).SingleOrDefault();
            if (data == null) return RedirectToAction("Index");
            db.Carts.Remove(data);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public IActionResult Order()
        {
            int? uid = HttpContext.Session.GetInt32("userid");
            if (uid == null)
            {
                return RedirectToAction("login", "user");
            }
            db = new icreamContext();
            var carts = db.Carts.Where(u => u.user_id == uid).Include(p => p.product).ToList();
            return View(carts);
        }
    }
}
