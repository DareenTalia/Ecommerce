using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyEcommerce.Areas.Admin.Models;
using MyEcommerce.Models;

namespace MyEcommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,user")]
    // [Authorize(Roles = "Admin,user")]
    public class AdministrationController : Controller
    {

        private readonly UserManager<MyUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly SignInManager<MyUser> signInManager;

        private readonly INotyfService _notyf;



        public AdministrationController(UserManager<MyUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<MyUser> signInManager, INotyfService notyf)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
            _notyf = notyf;
        }


        public IActionResult Index()
        {
            return View();
        }

        //-----------------User start  ---------------------------

        [HttpGet]
        public IActionResult CreateUser()
        {

            return View();

        }
        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserViewModel user, List<IFormFile> Files)
        {

            if (ModelState.IsValid)
            {
                //foreach (var file in Files)
                //{
                //    if (file.Length > 0)
                //    {
                //        string ImageName = Guid.NewGuid().ToString() + ".jpg";
                //        var filePaths = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Uploads", ImageName);
                //        using (var stream = System.IO.File.Create(filePaths))
                //        {
                //            await file.CopyToAsync(stream);
                //        }

                //         user.ImageName =  ImageName;
                //    }
                //}


                var appUser = new MyUser
                {

                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Country = user.Country,
                    Address = user.Address,
                    City = user.City,
                    PhoneNumber = user.Phone,
                    Email = user.Email,
                    UserName = user.Email,
                    // ImageName = user.ImageName,




                    //MyPassword = user.Password
                };

                IdentityResult result = await userManager.CreateAsync(appUser, user.Password);
                if (result.Succeeded)
                {
                    //if (signInManager.IsSignedIn(User) && User.IsInRole("Admin"))
                    //{
                    //    return RedirectToAction("ListUsers", "Administration");
                    //}

                    return RedirectToAction("ListUsers");
                }

                //else
                //{
                //    foreach (IdentityError error in result.Errors)
                //        ModelState.AddModelError("", error.Description);
                //}

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

            }
            return View(user);

        }


        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<IActionResult> IsEmailInUse(string email)
        {
            var user = await userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return Json(true);
            }
            else
            {
                return Json($"Email {email} is already in use.");
            }

        }

        //-----------------Edit user -----------------------

        [HttpGet]
        public async Task<IActionResult> EditUser(string id)
        {
            var user = await userManager.FindByIdAsync(id);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {id} cannot be found";
                return View("NotFound");
            }

            // GetClaimsAsync retunrs the list of user Claims
            var userClaims = await userManager.GetClaimsAsync(user);
            // GetRolesAsync returns the list of user Roles
            var userRoles = await userManager.GetRolesAsync(user);

            var model = new EditUserViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Country = user.Country,
                Address = user.Address,
                City = user.City,
                Phone = user.PhoneNumber,
                Email = user.Email,
                UserName = user.Email,

                Claims = userClaims.Select(c => c.Value).ToList(),
                Roles = userRoles
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserViewModel model, List<IFormFile> Files)
        {
            var user = await userManager.FindByIdAsync(model.Id);



            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {model.Id} cannot be found";
                return View("NotFound");
            }
            else
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

                        user.ImageName = ImageName;
                    }
                }

                user.Email = model.Email;
                user.UserName = model.UserName;
                user.City = model.City;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Country = model.Country;
                user.Address = model.Address;
                user.City = model.City;
                user.PhoneNumber = model.Phone;
                user.ImageName = user.ImageName;



                var result = await userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListUsers");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(model);
            }




        }

        [HttpGet]
        public async Task<IActionResult> ManageUserRoles(string userId)
        {
            ViewBag.userId = userId;

            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {userId} cannot be found";
                return View("NotFound");
            }

            var model = new List<UserRolesViewModel>();

            foreach (var role in roleManager.Roles.ToList())
            {
                var userRolesViewModel = new UserRolesViewModel
                {
                    RoleId = role.Id,
                    RoleName = role.Name
                };

                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    userRolesViewModel.IsSelected = true;
                }
                else
                {
                    userRolesViewModel.IsSelected = false;
                }

                model.Add(userRolesViewModel);
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ManageUserRoles(List<UserRolesViewModel> model, string userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {userId} cannot be found";
                return View("NotFound");
            }

            var roles = await userManager.GetRolesAsync(user);
            var result = await userManager.RemoveFromRolesAsync(user, roles);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot remove user existing roles");
                return View(model);
            }

            result = await userManager.AddToRolesAsync(user,
                model.Where(x => x.IsSelected).Select(y => y.RoleName));

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot add selected roles to user");
                return View(model);
            }

            return RedirectToAction("EditUser", new { Id = userId });
        }

        [HttpGet]
        public IActionResult ListUsers()
        {
            var users = userManager.Users;
            return View(users);
        }


        [HttpPost]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await userManager.FindByIdAsync(id);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {id} cannot be found";
                return View("NotFound");
            }
            else
            {
                var result = await userManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListUsers");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View("ListUsers");
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }
        //-----------------User end  ---------------------------

        //--------------------Role start -----------------------

        [HttpGet]
        public IActionResult ListRoles()
        {
            var roles = roleManager.Roles;
            return View(roles);
        }



        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                // We just need to specify a unique role name to create a new role
                IdentityRole identityRole = new IdentityRole
                {
                    Name = model.RoleName
                };

                // Saves the role in the underlying AspNetRoles table
                IdentityResult result = await roleManager.CreateAsync(identityRole);

                if (result.Succeeded)
                {
                    _notyf.Success("Role Added Succefully 😍");
                    //_notyf.Success("Item added to your Cart Successfuly 😍");
                    //_notyf.Custom("Custom Notification - closes in 5 seconds.", 5, "whitesmoke", "fa fa-gear");
                    //_notyf.Custom("Custom Notification - closes in 5 seconds.", 10, "#B600FF", "fa fa-home");

                    return RedirectToAction("ListRoles");
                }

                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);
        }


        // Role ID is passed from the URL to the action
        [HttpGet]
        public async Task<IActionResult> EditRole(string id)
        {
            // Find the role by Role ID
            var role = await roleManager.FindByIdAsync(id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {id} cannot be found";
                return View("NotFound");
            }

            var model = new EditRoleViewModel
            {
                Id = role.Id,
                RoleName = role.Name
            };

            // Retrieve all the Users
            foreach (var user in userManager.Users.ToList())
            {
                // If the user is in this role, add the username to
                // Users property of EditRoleViewModel. This model
                // object is then passed to the view for display
                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    model.Users.Add(user.UserName);
                }
            }

            return View(model);
        }

        // This action responds to HttpPost and receives EditRoleViewModel
        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {
            var role = await roleManager.FindByIdAsync(model.Id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {model.Id} cannot be found";
                return View("NotFound");
            }
            else
            {
                role.Name = model.RoleName;

                // Update the Role using UpdateAsync
                var result = await roleManager.UpdateAsync(role);

                if (result.Succeeded)
                {
                    _notyf.Success("Role is edited Succefully ");
                    return RedirectToAction("ListRoles");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(model);
            }
        }


        [HttpGet]
        public async Task<IActionResult> EditUsersInRole(string roleId)
        {
            ViewBag.roleId = roleId;

            var role = await roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {roleId} cannot be found";
                return View("NotFound");
            }

            var model = new List<UserRoleViewModel>();

            foreach (var user in userManager.Users.ToList())
            {
                var userRoleViewModel = new UserRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };

                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    userRoleViewModel.IsSelected = true;
                }
                else
                {
                    userRoleViewModel.IsSelected = false;
                }

                model.Add(userRoleViewModel);
            }

            return View(model);
        }



        [HttpPost]
        public async Task<IActionResult> EditUsersInRole(List<UserRoleViewModel> model, string roleId)
        {
            var role = await roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {roleId} cannot be found";
                return View("NotFound");
            }

            for (int i = 0; i < model.Count; i++)
            {
                var user = await userManager.FindByIdAsync(model[i].UserId);

                IdentityResult result = null;

                if (model[i].IsSelected && !(await userManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await userManager.AddToRoleAsync(user, role.Name);
                }
                else if (!model[i].IsSelected && await userManager.IsInRoleAsync(user, role.Name))
                {
                    result = await userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }

                if (result.Succeeded)
                {
                    if (i < (model.Count - 1))
                        continue;
                    else
                        return RedirectToAction("EditRole", new { Id = roleId });
                }
            }

            return RedirectToAction("EditRole", new { Id = roleId });
        }


        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            IdentityRole role = await roleManager.FindByIdAsync(id);


            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {id} cannot be found";
                return View("NotFound");
            }
            else
            {
                var result = await roleManager.DeleteAsync(role);

                if (result.Succeeded)
                {
                    _notyf.Success("Role Deleted  Succefully ");
                    return RedirectToAction("ListRoles");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View("ListRoles");
            }
        }

        //if (role != null)
        //{
        //    IdentityResult result = await roleManager.DeleteAsync(role);
        //    if (result.Succeeded)
        //    {
        //        return RedirectToAction("ListRoles");


        //    }
        //    _notyf.Success("Role Deleted  Succefully ");
        //    //else
        //    // Errors(result);
        //}
        //else
        //    ModelState.AddModelError("", "No role found");
        //return View("Index", roleManager.Roles);

    //  }
        //------------------------------------------------------




































        //public IActionResult Login(string ReturnUrl)
        //{
        //    return View(new UserViewModel()

        //    {
        //        ReturnUrl = ReturnUrl

        //    });

        //}

        //[HttpPost]
        //public async Task<IActionResult> Login(UserViewModel userModel)
        //{
        //    var result = await signInManager.PasswordSignInAsync(userModel.Email, userModel.Password, true, true);

        //    if (string.IsNullOrEmpty(userModel.ReturnUrl))
        //    {
        //        userModel.ReturnUrl = "~/";
        //    }
        //    if (result.Succeeded)
        //    {

        //        return Redirect(userModel.ReturnUrl);
        //    }

        //    else
        //    {
        //        return View("Login", userModel);

        //    }

        //}



        ////public async Task<IActionResult> LogOut()
        ////{

        ////    await signInManager.SignOutAsync();
        ////    return Redirect("~/");

        ////}


        ////public IActionResult Register()
        ////{
        ////    return View();

        ////}


        ////[HttpPost]
        ////public async Task<IActionResult> Register(UserViewModel userModel)
        ////{


        ////    var user = new MyUser()
        ////    {

        ////        FirstName = userModel.FirstName,
        ////        LastName = userModel.LastName,
        ////        Country = userModel.Country,
        ////        Address = userModel.Address,
        ////        City = userModel.City,
        ////        PhoneNumber = userModel.Phone,
        ////        Email = userModel.Email,
        ////        UserName = userModel.Email,
        ////        MyPassword = userModel.Password
        ////    };

        ////    var result = await userManger.CreateAsync(user, userModel.Password);

        ////    //if u want to give any user register default role 
        ////    //await userManger.AddToRoleAsync(user , "user");

        ////    if (result.Succeeded)
        ////    {
        ////        return Redirect("~/");
        ////    }
        ////    else
        ////    {

        ////        return View("Register", userModel);

        ////    }


        ////}















    }

}

