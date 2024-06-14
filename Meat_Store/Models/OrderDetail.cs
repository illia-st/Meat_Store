using System;
using System.Collections.Generic;

namespace Meat_Store.Models
{
    public partial class OrderDetail
    {
        public int Id { get; set; }
        public int MeatId { get; set; }
        public int Price { get; set; }
        public int OrderId { get; set; }

        public virtual Meat Meat { get; set; } = null!;
        public virtual Order Order { get; set; } = null!;
    }
}
