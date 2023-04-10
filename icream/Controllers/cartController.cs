using icream.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace icream.Controllers
{
    public class cartController : Controller
    {
        icreamContext db;

        public cartController(icreamContext db)
        {
            this.db = db;
        }
        public IActionResult Index()
        {
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
            var carts = db.Carts.Where(u => u.user_id == uid).Include(p => p.product).ToList();
            if (carts != null)
            {
                foreach (var cart in carts)
                {
                    Order order = new Order();
                    order.user_id = uid;
                    order.product_id = cart.product_id;
                    db.Orders.Add(order);
                }
                db.SaveChanges();
                db.Carts.RemoveRange(carts);
                db.SaveChanges();
            }
            return RedirectToAction("Index", "orders");
        }
    }
}
