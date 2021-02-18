using System;
using System.Collections.Generic;



namespace Domains
{
    public partial class TbCustomer
    {
        public TbCustomer()
        {
            TbCusomerItems = new HashSet<TbCusomerItem>();
            TbSalesInvoices = new HashSet<TbSalesInvoice>();
        }

        public int CustomerId { get; set; }
        public string CustomerName { get; set; }

        public virtual TbBusniessInfo TbBusniessInfo { get; set; }
        public virtual ICollection<TbCusomerItem> TbCusomerItems { get; set; }
        public virtual ICollection<TbSalesInvoice> TbSalesInvoices { get; set; }
    }
}
