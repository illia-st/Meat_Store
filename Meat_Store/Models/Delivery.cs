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
        public string DeliveryServise { get; set; } = null!;
        public string City { get; set; } = null!;

        public virtual ICollection<Order> Orders { get; set; }
    }
}
