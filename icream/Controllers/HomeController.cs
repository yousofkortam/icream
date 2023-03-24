using icream.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace icream.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            int? userId = HttpContext.Session.GetInt32("userid");
            if (userId == null)
            {
                ViewBag.userLoged = false;
            }else
            {
                ViewBag.userLoged = true;
            }
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}