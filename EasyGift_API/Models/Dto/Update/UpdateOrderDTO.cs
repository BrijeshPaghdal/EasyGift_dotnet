using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyGift_API.Models.Dto.Update
{
    public class UpdateOrderDTO
    {
        [Required]
        public int Id { get; set; }
        public int OrderId { get; set; }
        [ForeignKey("Customer")]
        
        public int CustomerId { get; set; }
        [ForeignKey("Product")]
        
        public int ProductId { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string PhoneNo { get; set; }
        
        public int Quantity { get; set; }
        
        public int TotalPrice { get; set; }
        [ForeignKey("PaymentType")]
        
        public int PaymentId { get; set; }
        
        public DateTime OrderDate { get; set; }
        
        public int Status { get; set; } = 0;
        
        public int PaymentStatus { get; set; } = 0;
    }
}
