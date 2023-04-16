using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EasyGift_API.Models.Dto.Update
{
    public class UpdateCustomerDTO
    {
        [Required]
        public int Id { get; set; }
        
        [MaxLength(20)]
        public string CustomerName { get; set; }
        
        [MaxLength(11)]
        public string MobileNo { get; set; }
        
        [ForeignKey("CustomerLogin")]
        public int CustomerLoginId { get; set; }
        
        public int CustomerStatus { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
