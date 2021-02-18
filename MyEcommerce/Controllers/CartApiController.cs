using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using Domains;
using Microsoft.AspNetCore.Mvc;
using MyEcommerce.Bl;
using MyEcommerce.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyEcommerce.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CartApiController : ControllerBase
    {

        private readonly IItemsServices itemsServices;
        private readonly INotyfService _notyf;

        public CartApiController(IItemsServices itemsServices , INotyfService notyf)
        {
            this.itemsServices = itemsServices;
            _notyf = notyf;
        }

        // GET: api/<CartApiController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<CartApiController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CartApiController>
        [HttpPost]
        public void Post([FromBody] CartItemsViewModel item )
        {
            CartViewModel cart = HttpContext.Session.GetObjectFromJson<CartViewModel>("Cart");
            if (cart == null)
                cart = new CartViewModel();
            //TbItem item = itemsServices.GetById(id);

            CartItemsViewModel cartItemsViewModel = cart.ListItems.Where(a => a.ItemId == item.ItemId).FirstOrDefault();
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
                    Price = item.Price,
                    Qty = 1,
                    Total = item.Price

                });

        }



        cart.Total = cart.ListItems.Sum(a => a.Total);
        HttpContext.Session.SetObjectAsJson("Cart", cart);
       
        //return Redirect("/Home/Index");



        }

        // PUT api/<CartApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CartApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
