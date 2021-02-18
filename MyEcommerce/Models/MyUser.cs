using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyEcommerce.Models
{

    
    public class MyUser:IdentityUser
    {


     
        public string FirstName { get; set; }
       
        public string LastName { get; set; }
       
        public string MyPassword  { get; set; }
     
        public string Country { get; set; }
        public string Address { get; set; }
        public string City { get; set; }

        public string Gender { get; set; }
        public int  Age    { get; set; }

        public string ImageName { get; set; }


    }
}
