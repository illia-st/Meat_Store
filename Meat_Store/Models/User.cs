using System;
using System.Collections.Generic;

namespace Meat_Store.Models
{
    public partial class User
    {
        public User()
        {
            FavouritePositions = new HashSet<FavouritePosition>();
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string ShopCartId { get; set; } = null!;

        public virtual ICollection<FavouritePosition> FavouritePositions { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
