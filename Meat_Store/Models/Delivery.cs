using System;
using System.Collections.Generic;

namespace Meat_Store.Models
{
    public partial class Delivery
    {
        public Delivery()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string DeliveryType { get; set; } = null!;
        public string? DeliveryService { get; set; }
        public string? City { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
