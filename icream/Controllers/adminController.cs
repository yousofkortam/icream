using icream.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace icream.Controllers
{
    public class adminController : Controller
    {
        icreamContext db;
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult login(string username, string password)
        {
            db = new icreamContext();
            var user = db.Users.Where(u => u.username == username && u.password == password && u.role == 0).FirstOrDefault();
            if (user == null)
            {
                ViewBag.status = "Icorrect username or password";
                return RedirectToAction("login");
            }
            HttpContext.Session.SetInt32("userid", user.id);
            return RedirectToAction("Index");
        }

        public IActionResult addMinAdmin()
        {
            return View();
        }

        [HttpPost]
        public IActionResult addAdmin(User user)
        {
            db = new icreamContext();
            var exist = db.Users.Where(n => n.username == user.username).SingleOrDefault();
            if (exist != null)
            {
                ViewBag.UsernameIsExist = "username already in use";
                return View();
            }

            user.role = 0; // 0 -> admin
            user.created_at = DateTime.Now;
            user.image = "/attachs/image/Profiles/Default-photo.jpg";
            db.Users.Add(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult addProduct()
        {
            return View();
        }

        [HttpPost]
        public IActionResult addProduct(Product product)
        {
            db = new icreamContext();
            db.Products.Add(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult editProduct()
        {
            return View();
        }

        [HttpPost]
        public IActionResult editProduct(Product product)
        {
            db = new icreamContext();
            db.Entry(product).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult deleteProduct(int id)
        {
            db = new icreamContext();
            var product = db.Products.Where(p => p.id == id).FirstOrDefault();
            if (product == null)
            {
                ViewBag.DeleteProductState = "Product not found";
                return RedirectToAction("Index");
            }
            db.Products.Remove(product);
            db.SaveChanges();
            return View();
        }


    }
}
