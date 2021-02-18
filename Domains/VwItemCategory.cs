using System;
using System.Collections.Generic;


namespace Domains
{
    public partial class VwItemCategory
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public decimal SalesPrice { get; set; }
        public decimal PurchasePrice { get; set; }
        public string ImageName { get; set; }
        public string CategoryName { get; set; }
        public int CategoryId { get; set; }
    }
}
