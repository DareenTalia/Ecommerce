using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyEcommerce.Areas.Admin.Models
{
    public class EditUserViewModel
    {
        public EditUserViewModel()
        {
            Claims = new List<string>();
            Roles = new List<string>();
        }

        public string Id { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [Remote(action: "IsEmailInUse", controller: "Administration")]
        public string Email { get; set; }

        public string City { get; set; }

      
        public string Password { get; set; }
        public string PasswordNotHashed { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }

        public string Phone { get; set; }



        public List<string> Claims { get; set; }

        public IList<string> Roles { get; set; }
    }
}
