using Microsoft.AspNetCore.Mvc;

namespace icream.Controllers
{
    public class serviceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
