using icream.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace icream.Controllers
{
    public class userController : Controller
    {
        icreamContext db;

        public IActionResult login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult login(string username, string password)
        {
            // TODO
            db = new icreamContext();
            var getUser = db.Users.Where(n => n.username == username && n.password == password).ToList().SingleOrDefault();
            if (getUser == null)
            {
                ViewBag.status = "Incorrect username or password";
                return View();
            }
            HttpContext.Session.SetInt32("userid", getUser.id);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult register(User user)
        {
            // TODO
            if (user.image == null)
            {
                user.image = "/attachs/image/Profiles/Default-photo.jpg";
            }
            db = new icreamContext();
            db.Users.Add(user);
            db.SaveChanges();
            return RedirectToAction("login");
        }

        public IActionResult logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("login");
        }

        public IActionResult profile(int id)
        {
            int? userId = HttpContext.Session.GetInt32("userid");
            if (userId == null)
            {
                return RedirectToAction("login");
            }
            ViewBag.isTheUser = (id == userId);
            db = new icreamContext();
            var userData = db.Users.Where(n => n.id == id).ToList().SingleOrDefault();

            return View(userData);
        }

        [HttpPost]
        public IActionResult profile(User user, IFormFile img)
        {
            // TODO
            user.id = (int)HttpContext.Session.GetInt32("userid");
            db = new icreamContext();
            var user_data = db.Users.Where(n => n.id == user.id).ToList().SingleOrDefault();
            if (user_data == null)
            {
                return RedirectToAction("profile");
            }
            if (img != null)
            {
                string path = $"wwwroot/attachs/image/Profiles/{img.FileName}";
                FileStream fileStream = new FileStream(path, FileMode.Create);
                img.CopyTo(fileStream);
                user_data.image = $"/attachs/image/Profiles/{img.FileName}";
            }
            user_data.name = user.name;
            user_data.email = user.email;
            user_data.age = user.age;
            user_data.address = user.address;
            user_data.city = user.city;
            user_data.country = user.country;
            user_data.postal_code = user.postal_code;
            user_data.bio = user.bio;
            db.SaveChanges();
            return RedirectToAction("profile");
        }
    }
}
