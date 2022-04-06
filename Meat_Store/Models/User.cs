using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Meat_Store.Models
{
    public partial class User: IdentityUser
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; }
        public string ShopCartId { get; set; } = null!;
    }
}
