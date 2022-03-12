using System;
using System.Collections.Generic;

namespace Meat_Store.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public int DeliveryId { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public DateTime OrderTime { get; set; }
        public int UserId { get; set; }

        public virtual Delivery Delivery { get; set; } = null!;
        public virtual User User { get; set; } = null!;
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
