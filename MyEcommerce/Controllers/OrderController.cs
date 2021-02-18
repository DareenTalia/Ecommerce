using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyEcommerce.Models;

namespace MyEcommerce.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Cart()
        {
            CartViewModel cart = HttpContext.Session.GetObjectFromJson<CartViewModel>("Cart");
            if (cart == null)
                cart = new CartViewModel();
            return View(cart);
        }

        public IActionResult RemoveItem(int id)
        {
            CartViewModel cartViewModel = HttpContext.Session.GetObjectFromJson<CartViewModel>("Cart");
            cartViewModel.ListItems.Remove(cartViewModel.ListItems.Where(a => a.ItemId == id).FirstOrDefault());
            cartViewModel.Total = cartViewModel.ListItems.Sum(a => a.Total);
            HttpContext.Session.SetObjectAsJson("Cart", cartViewModel);
            return RedirectToAction("Cart");
        }
    }
}
