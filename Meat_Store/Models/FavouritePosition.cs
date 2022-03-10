using System;
using System.Collections.Generic;

namespace Meat_Store.sakila
{
    public partial class FavouritePosition
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int MeatId { get; set; }

        public virtual Meat Meat { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
