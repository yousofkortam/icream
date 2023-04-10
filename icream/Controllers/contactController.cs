using icream.Models;
using Microsoft.AspNetCore.Mvc;

namespace icream.Controllers
{
    public class contactController : Controller
    {
        icreamContext db;

        public contactController(icreamContext db)
        {
            this.db = db;
        }

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
            contact.user_id = HttpContext.Session.GetInt32("userid");
            contact.created_at = DateTime.Now;
            db.Contacts.Add(contact);
            db.SaveChanges();
            ViewBag.contactStatus = "Message sent successfully";
            return View();
        }
    }
}
