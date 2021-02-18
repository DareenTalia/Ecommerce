using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyEcommerce.Models;
using Domains;

namespace MyEcommerce.Bl
{

    public interface IItemsServices
    {

        List<VwItemCategory> GetAllItems();
        
        List<TbItem> GetAll(); //  =  IEnumerable<TbItem> GetAll();
        TbItem GetById(int id);
        bool Add(TbItem item);
        bool Edit(TbItem item);
        bool Delete(int itemId);

        TbItem GetByIdWithImages(int id);
        List<TbItem> GetUpSellItems();
        List<TbItem> GetRelatedItems(decimal price);





    }

    public class clsItems: IItemsServices
    {


        private readonly EcommerceWebsiteContext ctx;

        public clsItems(EcommerceWebsiteContext context)
        {
            this.ctx  = context;
        }
        #region GetAllItems Method
        public List<TbItem> GetAll()
        {
            //-----------Lambada Linq-------------Include(a=>a.Category) هذه الطريقه الثانيه لعرض بيانات من جدولين بينهم علاقه------

            List<TbItem> items = ctx.TbItems
                .OrderByDescending(b => b.CreationDate).Include(a=>a.Category)
                .ToList();
            return items;

        }
        #endregion

        public List<VwItemCategory> GetAllItems()
        {
            List<VwItemCategory> lstItems = ctx.VwItemCategories.ToList();
            return lstItems;
        }
        public TbItem GetById(int id)
        {
        
           
            TbItem item = ctx.TbItems.FirstOrDefault(a=>a.ItemId == id);

            return item;

        }

        public bool Add(TbItem item)
        {
            try
            {
                
                //ctx.TbItems.Add(item);
                ctx.Add<TbItem>(item);
                ctx.SaveChanges();
                return true;
            }
            catch(Exception ex )
            {
                return false;
            }
           
        }

        public bool Edit(TbItem item)
        {
            try
            {

                // الطريقيتن التاليتين شغالين عن تجربه 

                // الطريقه الاولى للتعديل ع البيانات  وحفظ التعديل ف الداتا بيز
                
                //TbItem oldItem = ctx.TbItems.FirstOrDefault(a => a.ItemId == item.ItemId);
                //oldItem.CategoryId = item.CategoryId;
                //oldItem.ItemName = item.ItemName;
                //oldItem.SalesPrice = item.SalesPrice;
                //oldItem.PurchasePrice = item.PurchasePrice;
                //ctx.SaveChanges();


                // Method 2
             
                ctx.Entry(item).State = EntityState.Modified;
                ctx.SaveChanges();



                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }


        public bool Delete(int itemId)
        {
            try
            {

                // Method 2 this method work 
                // ولازم تغير الباراميترات كالتالى اعتقد هتلاقيهم متغيرين لانه الطريقه دى شغاله
                // public bool Delete(int itemId)
              
                TbItem oldItem = ctx.TbItems.FirstOrDefault(a => a.ItemId == itemId);
                ctx.TbItems.Remove(oldItem);
                ctx.SaveChanges();



                // هذه الطريقه لا تعمل not work 
                // لاستخدام هذه الطريقه غير الباراميترات كالتالى 
                //   public bool Delete(TbItem item)
               
                //ctx.Entry(item).State = EntityState.Deleted;
                //ctx.SaveChanges();



                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }



        //Details View Page  ___ DetailsViewModel

        public TbItem GetByIdWithImages(int id)
        {
            try
            {
                TbItem item = ctx.TbItems.Include(a => a.TbItemImages).FirstOrDefault(a => a.ItemId == id);
                return item;
            }
            catch (Exception ex)
            {
                return new TbItem();
            }

        }

        public List<TbItem> GetUpSellItems()
        {
            var query = (from items in ctx.TbItems
                         join
                         discount in ctx.TbItemDiscounts
                         on items.ItemId equals discount.ItemId
                         where discount.EndDate >= DateTime.Now
                         select items).Include(a => a.Category);
            return query.ToList();
        }
        /// <summary>
        /// get realted items according to price range
        /// </summary>
        /// <param name="price"></param>
        /// <returns></returns>
        public List<TbItem> GetRelatedItems(decimal price)
        {
            decimal start = price - 3000;
            decimal end = price + 3000;
            List<TbItem> lstItems = ctx.TbItems.Include(a => a.Category).Where(a => a.SalesPrice >= start && a.SalesPrice <= end).
            OrderByDescending(a => a.CreationDate).ToList();
            return lstItems;
        }
    }
}
