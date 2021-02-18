using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domains;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyEcommerce.Bl;
using MyEcommerce.Models;
using Newtonsoft.Json;

namespace MyEcommerce.Controllers
{
    //[Authorize]
    public class ItemController : Controller
    {
        private readonly IItemsServices itemsServices;

        public ItemController(IItemsServices itemsServices )
        {
            this.itemsServices = itemsServices;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details(int id)
        {
            DetailsViewModel model = new DetailsViewModel();
            model.Item = itemsServices.GetByIdWithImages(id);
            model.ListRelatedItems = itemsServices.GetRelatedItems(model.Item.SalesPrice);
            model.ListUpSellItems = itemsServices.GetUpSellItems();

            return View(model);
        }



     


            public IActionResult AddToCart(int id)
        {
            
            CartViewModel cart = HttpContext.Session.GetObjectFromJson<CartViewModel>("Cart");
            if (cart == null)
                cart = new CartViewModel();
            TbItem item = itemsServices.GetById(id);

            CartItemsViewModel cartItemsViewModel = cart.ListItems.Where(a => a.ItemId == id).FirstOrDefault();
            if (cartItemsViewModel != null)
            {
                cartItemsViewModel.Qty++;
                cartItemsViewModel.Total = cartItemsViewModel.Price * cartItemsViewModel.Qty;
            }
            else
            {
                cart.ListItems.Add(new CartItemsViewModel()
                {
                    ItemId = item.ItemId,
                    ItemName = item.ItemName,
                    ImageName = item.ImageName,
                    Price = item.SalesPrice,
                    Qty = 1,
                    Total = item.SalesPrice

                });

            }

       

            cart.Total = cart.ListItems.Sum(a => a.Total);
            HttpContext.Session.SetObjectAsJson("Cart", cart);
            return Redirect("/Home/Index");

        }

        //public IActionResult AddToCart(int id)
        //{


        //    //how to store string in session  key - value 
        //    //HttpContext.Session.GetString("Cart");
        //    //HttpContext.Session.SetString("Cart" , "any string");

        //    // how to store object in session to store object should to convert it to json file 
        //    // and to get this object again should convert this json file to object again
        //    // means u will make Sealize and Desealize
        //    //CartViewModel cart = new CartViewModel();
        //    //// Making the Key 
        //    //string  any =  HttpContext.Session.GetString("Cart");
        //    //// convert json to object >> Desealize 
        //    //cart = JsonConvert.DeserializeObject<CartViewModel>(any);
        //    //// convert object to json >> Sealize 
        //    //HttpContext.Session.SetString("Cart", JsonConvert.SerializeObject(cart));




        //    //   Using  The Ready Class >> SessionExtensions Class 
        //    CartViewModel cart = new CartViewModel();
        //    // convert json to object >> Desealize 
        //    cart = HttpContext.Session.GetObjectFromJson<CartViewModel>("Cart");
        //    // convert object to json >> Sealize 
        //    HttpContext.Session.SetObjectAsJson("Cart", cart);

        //    return View();
        //}



        public IActionResult Shop()
        {
            return View();
        }
        }
    }
