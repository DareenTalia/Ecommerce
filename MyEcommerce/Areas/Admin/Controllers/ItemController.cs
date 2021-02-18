using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyEcommerce.Bl;

using Domains;
using Microsoft.AspNetCore.Authorization;

namespace MyEcommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = "Admin")]
    [Authorize]
    public class ItemController : Controller
    {

        private readonly IItemsServices itemsServices;
        private readonly ICategoriesService categoriesService;
        //private readonly UserManager<ApplicationUser> userManager;

        public ItemController(IItemsServices itemsServices , ICategoriesService categoriesService)
        {
            this.itemsServices = itemsServices;
            this.categoriesService = categoriesService;
           
        }
        [AllowAnonymous]
        public IActionResult List()
        {
          
            
            return View(itemsServices.GetAll());
        }


        public IActionResult Edit(int?  id)
        {
           
            ViewBag.Categories = categoriesService.GetAll();
            if(id != null)
            {
               
                return View(itemsServices.GetById( Convert.ToInt32( id)));

            }
            else

            return View();
        }

        public IActionResult Delete(int Item)
        {

            itemsServices.Delete(Item);
            return RedirectToAction("List");
        }

        [HttpPost]
        public async Task<IActionResult> Save(TbItem Item , List<IFormFile> Files)
        {
            if(ModelState.IsValid)
            {
                foreach (var file in Files)
                {
                    if (file.Length > 0)
                    {
                        string ImageName = Guid.NewGuid().ToString() + ".jpg";
                        var filePaths = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Uploads", ImageName);
                        using (var stream = System.IO.File.Create(filePaths))
                        {
                            await file.CopyToAsync(stream);
                        }
                        Item.ImageName = ImageName;
                    }
                }

                
                if (Item.ItemId == 0)
                    itemsServices.Add(Item);
                else
                    itemsServices.Edit(Item);
                return RedirectToAction("List");




            }
            else
            {

              
                ViewBag.Categories = categoriesService.GetAll();
                return View("Edit", Item);
            }
                


        }

    }
}
