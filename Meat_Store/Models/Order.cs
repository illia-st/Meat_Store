using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Meat_Store.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }
        [BindNever]
        public int Id { get; set; }
        [MinLength(3)]
        [Required(ErrorMessage = "Дожина імені має бути не менше 3 символів")]
        public string Name { get; set; } = null!;
        [MinLength(3)]
        [Required(ErrorMessage = "Дожина імені має бути не менше 3 символів")]
        public string Surname { get; set; } = null!;
        public int DeliveryId { get; set; }
        [MinLength(10)]
        [Required(ErrorMessage = "Дожина номеру телефона має бути не менше 10 символів")]
        public string? PhoneNumber { get; set; }
        [MinLength(10)]
        [Required(ErrorMessage = "Дожина назви електронної адреси має бути не менше 10 символів")]
        public string Email { get; set; } = null!;
        [BindNever]
        [ScaffoldColumn(false)]
        public DateTime OrderTime { get; set; }
        public int UserId { get; set; }

        public virtual Delivery Delivery { get; set; } = null!;
        public virtual User User { get; set; } = null!;
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
