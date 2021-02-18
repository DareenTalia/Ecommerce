using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyEcommerce.Models
{
    public class CartViewModel
    {

        public CartViewModel()
        {
            ListItems = new List<CartItemsViewModel>();
        }
        public List<CartItemsViewModel> ListItems { get; set; }

        public decimal Total { get; set; }
    }
}
