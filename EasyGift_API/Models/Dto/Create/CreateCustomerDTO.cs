using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EasyGift_API.Models.Dto.Create
{
    public class CreateCustomerDTO
    {
        [Required]
        [MaxLength(20)]
        public string CustomerName { get; set; }
        [Required]
        [MaxLength(11)]
        public string MobileNo { get; set; }
        [Required]
        [ForeignKey("CustomerLogin")]
        public int CustomerLoginId { get; set; }
        [Required]
        public int CustomerStatus { get; set; }
        [Required]
        private DateTime CreatedDate { get; set; }
    }
}
