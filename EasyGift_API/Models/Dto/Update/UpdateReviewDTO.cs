using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyGift_API.Models.Dto.Update
{
    public class UpdateReviewDTO
    {
        [Required]
        public int Id { get; set; }
        
        [ForeignKey("Order")]
        public int OrderId { get; set; }
        
        public int Rating { get; set; }
        [MaxLength(1000)]
        public string ReviewDetail { get; set; }
        
        public DateTime ReviewDate { get; set; }

    }
}
