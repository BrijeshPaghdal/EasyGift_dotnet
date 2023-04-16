using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyGift_API.Models.Dto.Create
{
    public class CreateReviewDTO
    {

        [Required]
        [ForeignKey("Order")]
        public int OrderId { get; set; }
        [Required]
        public int Rating { get; set; }
        [Required]
        public string ReviewDetail { get; set; }
        [Required]
        public DateTime ReviewDate { get; set; }

    }
}
