﻿using icream.Models;
using Microsoft.AspNetCore.Mvc;

namespace icream.Controllers
{
    public class productController : Controller
    {
        icreamContext db;

        public productController(icreamContext db)
        {
            this.db = db;
        }

        public IActionResult Index()
        {
            List<Product> products = db.Products.ToList();
            return View(products);
        }

        public IActionResult AddToCart(int id)
        {
            int? user_id = HttpContext.Session.GetInt32("userid");
            if (user_id == null)
            {
                return RedirectToAction("login", "user");
            }
            Cart cart = new Cart();
            cart.product_id = id;
            cart.user_id = user_id;
            cart.checked_out = 0;
            db.Carts.Add(cart);
            db.SaveChanges();
            return RedirectToAction("Index", "cart");
        }
    }
}
