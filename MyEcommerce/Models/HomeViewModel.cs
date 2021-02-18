using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domains;

namespace MyEcommerce.Models
{
    public class HomeViewModel
    {

        public IEnumerable<TbSlider> ListSliderImages { get; set; }
        public IEnumerable<TbItem> ListAllItems { get; set; }
        public IEnumerable<TbItem> ListNewItems { get; set; }
        public IEnumerable<TbItem> ListBestSeller { get; set; }
        public IEnumerable<TbItem> ListCategories { get; set; }
    }
}
