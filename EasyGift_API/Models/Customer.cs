using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyGift_API.Models
{
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
        public DateTime? UpdatedDate { get; set; }

    }
}
