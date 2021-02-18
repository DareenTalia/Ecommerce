using System;
using System.Collections.Generic;



namespace Domains
{
    public partial class TbItemImage
    {
        public int ImageId { get; set; }
        public string ImageName { get; set; }
        public int ItemId { get; set; }

        public virtual TbItem Item { get; set; }
    }
}
