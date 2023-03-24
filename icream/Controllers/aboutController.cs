using Microsoft.AspNetCore.Mvc;

namespace icream.Controllers
{
    public class aboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
