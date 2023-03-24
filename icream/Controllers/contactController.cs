using Microsoft.AspNetCore.Mvc;

namespace icream.Controllers
{
    public class contactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
