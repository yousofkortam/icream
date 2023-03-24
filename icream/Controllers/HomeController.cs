using icream.Models;
using Microsoft.AspNetCore.Mvc;
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
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}