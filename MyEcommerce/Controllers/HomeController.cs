using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyEcommerce.Models;
using Domains;
using MyEcommerce.Bl;

namespace MyEcommerce.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IItemsServices itemsServices;

        public HomeController(ILogger<HomeController> logger , IItemsServices itemsServices)
        {
            _logger = logger;
            this.itemsServices = itemsServices;
        }


        public JsonResult Index2()

        {
            return Json(new {id="1" , Name="Ahmed" });
        }


           //  [Route("ahmed")]
            public IActionResult Index()

            {

          
            HomeViewModel model = new HomeViewModel();
            model.ListAllItems = itemsServices.GetAll();
            model.ListNewItems = model.ListAllItems.Take(8);
            model.ListBestSeller = model.ListAllItems.Take(3);
            model.ListCategories = model.ListAllItems.GroupBy(a => a.CategoryId).Select(a => a.First()).ToList();

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
