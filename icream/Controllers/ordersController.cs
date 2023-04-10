using icream.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace icream.Controllers
{
    public class ordersController : Controller
    {
        icreamContext db;

        public ordersController( icreamContext db )
        {
            this.db = db;
        }
        public IActionResult Index()
        {
            var uid = HttpContext.Session.GetInt32("userid");
            if (uid == null)
            {
                return RedirectToAction("login", "user");
            }
            var orders = db.Orders.Where(u => u.user_id == uid).Include(p => p.product).ToList();
            return View(orders);
        }

        public IActionResult cancel(int id)
        {
            var uid = HttpContext.Session.GetInt32("userid");
            if (uid == null)
            {
                return RedirectToAction("login", "user");
            }
            var order = db.Orders.Where(o => o.id == id).SingleOrDefault();
            if (order == null) 
            {
                return RedirectToAction("Index", "orders");
            }
            db.Orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index", "orders");
        }
    }
}
