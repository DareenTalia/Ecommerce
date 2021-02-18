using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyEcommerce.Models;
using Domains;

namespace MyEcommerce.Bl
{

    public interface ICategoriesService
    {
        List<TbCategory> GetAll();

    }
    public class clsCategories: ICategoriesService
    {

        EcommerceWebsiteContext ctx;
        public clsCategories(EcommerceWebsiteContext context )
        {
            this.ctx = context;
        }
        public List<TbCategory> GetAll()
        {
          
            
            return ctx.TbCategories.ToList();

        }

    }
}
