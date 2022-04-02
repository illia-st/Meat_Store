using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Meat_Store.Models
{
    public partial class User: IdentityUser
    {
        public User()
        {
            FavoutirePositions = new HashSet<FavoutirePosition>();
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; }
        public string ShopCartId { get; set; } = null!;

        public virtual ICollection<FavoutirePosition> FavoutirePositions { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
