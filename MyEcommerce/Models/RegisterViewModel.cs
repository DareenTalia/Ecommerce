using Microsoft.AspNetCore.Mvc;
using MyEcommerce.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyEcommerce.Models
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Remote(action: "IsEmailInUse", controller: "Account")]
        // [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", ErrorMessage = "Invalid Email format")]
        // [ValidEmailDomain(allowedDomain: "gmail.com", ErrorMessage = "Email domain must be gmail.com")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password",ErrorMessage = "Password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }



        [Required(ErrorMessage ="plz enter your First Name ")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "plz enter your Last Name ")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "plz enter your the country ")]
        public string Country { get; set; }
        [Required(ErrorMessage = "plz enter your Address ")]
        public string Address { get; set; }
        [Required(ErrorMessage = "plz enter your City ")]
        public string City { get; set; }
        [Required(ErrorMessage = "plz enter your Phone ")]
        public string Phone { get; set; }
        public string ReturnUrl { get; set; }








    }
}
