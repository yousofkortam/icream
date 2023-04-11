using icream.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace icream.Controllers
{
    public class adminController : Controller
    {
        icreamContext db;

        public adminController(icreamContext db)
        {
            this.db = db;
        }

        private bool isAdmin(int? uid)
        {
            string? role = HttpContext.Session.GetString("role");
            if (uid == null || role != "Admin")
            {
                return false;
            }
            return true;
        }

        public IActionResult dashboard()
        {
            int? uid = HttpContext.Session.GetInt32("userid");
            if (!isAdmin(uid))
            {
                return RedirectToAction("login", "user");
            }
            var products = db.Products.Include(n => n.category).ToList();
            return View(products);
        }

        public IActionResult add_admin()
        {
            int? uid = HttpContext.Session.GetInt32("userid");
            if (!isAdmin(uid))
            {
                return RedirectToAction("login", "user");
            }
            return View();
        }

        bool isValid(User user)
        {
            if (user.name == null || user.username == null || user.email == null || user.password == null || user.confirm_password == null || user.password != user.confirm_password) return false;
            return true;
        }

        [HttpPost]
        public IActionResult add_admin(User user)
        {
            if (!isValid(user))
            {
                return View();
            }
            var exist = db.Users.Where(n => n.username == user.username).SingleOrDefault();
            if (exist != null)
            {
                ViewBag.UsernameIsExist = "Username already in use";
                return View();
            }

            user.is_admin = true; // true -> admin
            user.created_at = DateTime.Now;
            user.image = "/attachs/image/Profiles/Default-photo.jpg";
            db.Users.Add(user);
            db.SaveChanges();
            return RedirectToAction("add_admin");
        }

        public IActionResult add_product()
        {
            int? uid = HttpContext.Session.GetInt32("userid");
            if (!isAdmin(uid))
            {
                return RedirectToAction("login", "user");
            }
            return View();
        }

        [HttpPost]
        public IActionResult add_product(Product product, IFormFile img)
        {
            string path = $"wwwroot/attachs/image/Products/{img.FileName}";
            FileStream fileStream = new FileStream(path, FileMode.Create);
            img.CopyTo(fileStream);
            product.image = $"/attachs/image/Products/{img.FileName}";
            db.Products.Add(product);
            db.SaveChanges();
            return RedirectToAction("dashboard");
        }

        public IActionResult editProduct(int id)
        {
            int? uid = HttpContext.Session.GetInt32("userid");
            if (!isAdmin(uid))
            {
                return RedirectToAction("login", "user");
            }
            var product = db.Products.Find(id);
            if (product == null)
            {
                return RedirectToAction("dashboard");
            }
            return View(product);
        }

        [HttpPost]
        public IActionResult editProduct(Product product, int id)
        {
            var old_product = db.Products.Find(id);
            if (old_product == null)
            {
                return RedirectToAction("dashboard");
            }
            if (product.image != null)
            {
                old_product.image = product.image;
            }
            old_product.name = product.name;
            old_product.price = product.price;
            old_product.category_id = product.category_id;
            db.SaveChanges();
            return RedirectToAction("dashboard");
        }

        public IActionResult deleteProduct(int id)
        {
            int? uid = HttpContext.Session.GetInt32("userid");
            if (!isAdmin(uid))
            {
                return RedirectToAction("login", "user");
            }
            var product = db.Products.Where(p => p.id == id).FirstOrDefault();
            if (product == null)
            {
                ViewBag.DeleteProductState = "Product not found";
                return RedirectToAction("dashboard");
            }
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("dashboard");
        }


        public IActionResult orders()
        {
            int? uid = HttpContext.Session.GetInt32("userid");
            if (!isAdmin(uid))
            {
                return RedirectToAction("login", "user");
            }
            var orders = db.Orders.Include(u => u.user).ToList();
            return View(orders);
        }


    }
}
