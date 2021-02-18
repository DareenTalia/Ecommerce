using Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyEcommerce.Models
{
    public class DetailsViewModel
    {

        public TbItem Item { get; set; }
        public List<TbItem> ListRelatedItems { get; set; }
        public List<TbItem> ListUpSellItems { get; set; }
    }
}
