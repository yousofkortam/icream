using icream.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace icream.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        icreamContext db;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            db = new icreamContext();
            List<Gallery> gallery = db.Galleries.ToList();
            ViewBag.gallery = gallery;
            List<Product> products = db.Products.ToList();
            ViewBag.products = products;
            List<Clients_say> reviews = db.Clients_says.Include(u => u.user).ToList();
            ViewBag.reviews = reviews;
            int? uid = HttpContext.Session.GetInt32("userid");
            if (uid != null)
            {
                var user = db.Users.Where(u => u.id == uid).Select(n => n.is_admin).SingleOrDefault();
                ViewBag.is_admin = user;
            }
            return View();
        }

        public IActionResult addReview(string userReview)
        {
            var uid = HttpContext.Session.GetInt32("userid");
            if (uid == null)
            {
                return RedirectToAction("login", "user");
            }

            db = new icreamContext();
            Clients_say Review = new Clients_say();
            Review.user_id = uid;
            Review.review = userReview;
            db.Clients_says.Add(Review);
            db.SaveChanges();

            return RedirectToAction("Index");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}