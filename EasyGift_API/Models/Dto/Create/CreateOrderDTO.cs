using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyGift_API.Models.Dto.Create
{
    public class CreateOrderDTO
    {
        [Required]
        public int OrderId { get; set; }
        [ForeignKey("Customer")]
        [Required]
        public int CustomerId { get; set; }
        [ForeignKey("Product")]
        [Required]
        public int ProductId { get; set; }
        [Required]
        [MaxLength(30)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(30)]
        public string LastName { get; set; }
        [Required]
        [MaxLength(20)]
        public string PhoneNo { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public int TotalPrice { get; set; }
        [ForeignKey("PaymentType")]
        [Required]
        public int PaymentId { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
        [Required]
        public int Status { get; set; } = 0;
        [Required]
        public int PaymentStatus { get; set; } = 0;
    }
}
