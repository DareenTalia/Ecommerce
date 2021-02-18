using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyEcommerce.Models;

namespace MyEcommerce.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserManager<MyUser> userManger;
        private readonly SignInManager<MyUser> signInManager;

        public UsersController(UserManager<MyUser> userManger , SignInManager<MyUser> signInManager)
        {
            this.userManger = userManger;
            this.signInManager = signInManager;

        }


        public IActionResult Login(string ReturnUrl)
        {
            return View(new UserViewModel()

            {
                ReturnUrl = ReturnUrl

            });

        }

        [HttpPost]
        public async Task<IActionResult> Login(UserViewModel userModel)
        {
            var result = await signInManager.PasswordSignInAsync(userModel.Email, userModel.Password, true, true);

            if (string.IsNullOrEmpty(userModel.ReturnUrl))
            {
                userModel.ReturnUrl = "~/";
            }
            if (result.Succeeded)
            {

                return Redirect(userModel.ReturnUrl);
            }

            else
            {
                return View("Login", userModel);

            }

        }



        public async Task<IActionResult> LogOut()
        {

            await signInManager.SignOutAsync();
            return Redirect("~/");

        }


        public IActionResult Register()
        {
            return View();

        }


        [HttpPost]
        public async Task<IActionResult> Register(UserViewModel userModel)
        {


            var user = new MyUser()
            {

                FirstName = userModel.FirstName,
                LastName = userModel.LastName,
                Country = userModel.Country,
                Address = userModel.Address,
                City = userModel.City,
                PhoneNumber = userModel.Phone,
                Email = userModel.Email,
                UserName = userModel.Email,
                MyPassword = userModel.Password
            };

            var result = await userManger.CreateAsync(user, userModel.Password);

            //if u want to give any user register default role 
            //await userManger.AddToRoleAsync(user , "user");

            if (result.Succeeded)
            {
                return Redirect("~/");
            }
            else
            {
                
                return View("Register", userModel);

            }


        }

    }
}
