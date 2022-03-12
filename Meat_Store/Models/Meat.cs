using System;
using System.Collections.Generic;

namespace Meat_Store.Models
{
    public partial class Meat
    {
        public Meat()
        {
            FavouritePositions = new HashSet<FavouritePosition>();
            OrderDetails = new HashSet<OrderDetail>();
            ShopCartItems = new HashSet<ShopCartItem>();
        }

        public int Id { get; set; }
        public string ShortDesc { get; set; } = null!;
        public string LongDesc { get; set; } = null!;
        public string Img { get; set; } = null!;
        public int Price { get; set; }
        public int Number { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; } = null!;

        public virtual Category Category { get; set; } = null!;
        public virtual ICollection<FavouritePosition> FavouritePositions { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<ShopCartItem> ShopCartItems { get; set; }
    }
}
