using System.ComponentModel.DataAnnotations;

namespace Meat_Store.Models
{
    public class FakeOrder
    {
        [StringLength(50)]
        [Required(ErrorMessage = "Це поле обов'язкове")]
        public string Name { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Це поле обов'язкове")]
        public string Surname { get; set; }

        public int DeliveryId { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Це поле обов'язкове")]
        public string? PhoneNumber { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Це поле обов'язкове")]
        public string Email { get; set; }

    }
}
