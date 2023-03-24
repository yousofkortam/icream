using icream.Models;
using Microsoft.AspNetCore.Mvc;

namespace icream.Controllers
{
    public class contactController : Controller
    {
        icreamContext db;
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(Contact contact)
        {
            // TODO
            if (HttpContext.Session.GetInt32("userid") == null)
            {
                return RedirectToAction("login", "user");
            }
            db = new icreamContext();
            contact.user_id = HttpContext.Session.GetInt32("userid");
            db.Contacts.Add(contact);
            db.SaveChanges();
            ViewBag.contactStatus = "Message sent successfully";
            return View();
        }
    }
}
