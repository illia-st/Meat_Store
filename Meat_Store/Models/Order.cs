using Microsoft.AspNetCore.Mvc.ModelBinding;
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
        
        [StringLength(50)]
        [Required(ErrorMessage = "asd")]
        public string Name { get; set; } = null!;

        
        [StringLength(50)]
        [Required(ErrorMessage = "asd")]
        public string Surname { get; set; } = null!;
        
        public int DeliveryId { get; set; }
        
        [StringLength(50)]
        [Required(ErrorMessage = "asd")]
        public string? PhoneNumber { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "asd")]
        public string Email { get; set; } = null!;
        
        [BindNever]
        [ScaffoldColumn(false)]
        public DateTime OrderTime { get; set; }
        
        public string? UserId { get; set; }

        public virtual Delivery Delivery { get; set; } = null!;
        
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        public Order Clone()
        {
            return new Order()
            {
                Name = (string)this.Name.Clone(),
                Surname = (string)this.Surname.Clone(),
                PhoneNumber = (string)this.PhoneNumber.Clone(),
                Email = (string)this.Email.Clone(),
            };
        }
    }
}
