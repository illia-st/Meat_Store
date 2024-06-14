using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Meat_Store.Models
{
    public class User: IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; } 
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
