using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyEcommerce.Models;

namespace MyEcommerce.Controllers
{
    public class AccountController : Controller


    {
        private readonly UserManager<MyUser> userManger;
        private readonly SignInManager<MyUser> signInManager;

        public AccountController(UserManager<MyUser> userManager,
            SignInManager<MyUser> signInManager)
        {
            this.userManger = userManager;
            this.signInManager = signInManager;
        }



        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Copy data from RegisterViewModel to IdentityUser
                var user = new MyUser
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Country = model.Country,
                    Address = model.Address,
                    City = model.City,
                    PhoneNumber = model.Phone,
                    Email = model.Email,
                    UserName = model.Email,
                    MyPassword = model.Password

                };

                // Store user data in AspNetUsers database table
                var result = await userManger.CreateAsync(user, model.Password);

                // If user is successfully created, sign-in the user using
                // SignInManager and redirect to index action of HomeController
                if (result.Succeeded)
                {
                    //isPersistent: false   >> لو قفلت المتصفح هتفقد تسجيل الحساب مش هيتخزن يعنى 
                    //isPersistent: true   >>  loggined حتى لو قفلت المتصفح هيفضل محتفظ بمعلومات التسجيل وهيبقى  
                    // this the scond paramter is boolean 
                    // we use this paramter to specify to create a session cookie or a permement cookie
                    //a session cookie  lost after clode the browser 
                    // a permement cookie is retained on the client machine even after the broswer closed
                    // we make it here false and when login u have Remember me Checkbox to select
                    // if u select remember me will save  your login in Cookie in client 
                    //   isPersistent: false .. ممكن تخليه يتحفظ ف الكوكيز ع طول من غير ما تعرف المستخدم  Register يعنى هنا ف  ال 
                    // بس اعتقد ف رساله بتظهر للمستخدم بتقوله هنستخدم الكويز اعتقد دى من المتصفح نفسه هنبقى نبحث
                    //    عملت زر تشيك بوكس عشن نسيب الاختيار للمستخدمLOGIN    هيحفظ ف الكوكيز اوتوماتيك ف ال  true يعنى لو عملتها 
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("index", "home");
                }

                // If there are any errors, add them to the ModelState object
                // which will be displayed by the validation summary tag helper
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }





        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<IActionResult> IsEmailInUse(string email)
        {
            var user = await userManger.FindByEmailAsync(email);

            if (user == null)
            {
                return Json(true);
            }
            else
            {
                return Json($"Email {email} is already in use.");
            }

        }


      

        [HttpPost]
       [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();

            //return View(new LoginViewModel()

            //{
            //    ReturnUrl = ReturnUrl

            //});
        }




        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model , string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                //model.RememberMe  this parameter ake boolean if u make it true will save your login in a cookie
                // permenet cookie in client if u make it false will save your login in server willnot store 
                // your login and  your login will come from server side not client called session cookie 
                // here we make checkbox Remember me to select if we choose true or false
                // if u Mark the checkbox means true not mark means false 
                // When u make logout the permenet cookie =  client cookie will removed 
                // the 4 parameter to lock the account on faliure 
                var result = await signInManager.PasswordSignInAsync(
                    model.Email, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(ReturnUrl) && Url.IsLocalUrl(ReturnUrl))
                    {
                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("index", "home");
                    }

                   
                }

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            }

            return View(model);
        }






    }
}
