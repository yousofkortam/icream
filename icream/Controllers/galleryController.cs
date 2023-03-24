using Microsoft.AspNetCore.Mvc;

namespace icream.Controllers
{
    public class galleryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
