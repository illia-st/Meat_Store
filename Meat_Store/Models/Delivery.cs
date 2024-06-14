using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace Meat_Store.Models
{
    public partial class Delivery
    {
        public Delivery()
        {
            Orders = new HashSet<Order>();
        }
        [BindNever]
        public int Id { get; set; }

        public int DeliveryType { get; set; }

        public string? City { get; set; }

        [BindNever]
        [ScaffoldColumn(false)]
        public DateTime OrderTime { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
