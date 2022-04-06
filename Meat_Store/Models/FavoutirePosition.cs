using System;
using System.Collections.Generic;

namespace Meat_Store.Models
{
    public partial class FavoutirePosition
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public int MeatId { get; set; }

        public virtual Meat Meat { get; set; } = null!;
    }
}
