using Microsoft.AspNetCore.Mvc;

namespace icream.Controllers
{
    public class productController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
