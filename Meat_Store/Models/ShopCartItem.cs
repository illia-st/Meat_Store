﻿using System;
using System.Collections.Generic;

namespace Meat_Store.Models
{
    public partial class ShopCartItem
    {
        public int Id { get; set; }
        public int MeatId { get; set; }
        public string ShopCartId { get; set; } = null!;

        public virtual Meat Meat { get; set; } = null!;
    }
}
