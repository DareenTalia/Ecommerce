using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Domains
{
    public partial class TbItem
    {
        public TbItem()
        {
            TbCusomerItems = new HashSet<TbCusomerItem>();
            TbItemDiscounts = new HashSet<TbItemDiscount>();
            TbItemImages = new HashSet<TbItemImage>();
            TbPurchaseInvoiceItems = new HashSet<TbPurchaseInvoiceItem>();
            TbSalesInvoiceItems = new HashSet<TbSalesInvoiceItem>();
        }

        public int ItemId { get; set; }

        [Required(ErrorMessage = "Please enter Item Name")]
        public string ItemName { get; set; }

        [Required(ErrorMessage = "Please enter Sales Price")]
        public decimal SalesPrice { get; set; }

        [Required(ErrorMessage = "Please enter Purchase Price")]
        public decimal PurchasePrice { get; set; }

        [Required(ErrorMessage = "Please Select a Category")]
        public int CategoryId { get; set; }
        public string ImageName { get; set; }
        public DateTime CreationDate { get; set; }

        public virtual TbCategory Category { get; set; }
        public virtual ICollection<TbCusomerItem> TbCusomerItems { get; set; }
        public virtual ICollection<TbItemDiscount> TbItemDiscounts { get; set; }
        public virtual ICollection<TbItemImage> TbItemImages { get; set; }
        public virtual ICollection<TbPurchaseInvoiceItem> TbPurchaseInvoiceItems { get; set; }
        public virtual ICollection<TbSalesInvoiceItem> TbSalesInvoiceItems { get; set; }
    }
}
