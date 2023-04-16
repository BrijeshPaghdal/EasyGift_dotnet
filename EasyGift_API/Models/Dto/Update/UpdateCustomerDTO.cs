using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EasyGift_API.Models.Dto.Update
{
    public class UpdateCustomerDTO
    {
        public int Id { get; set; }
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
        public DateTime CreatedDate { get; set; }
    }
}
