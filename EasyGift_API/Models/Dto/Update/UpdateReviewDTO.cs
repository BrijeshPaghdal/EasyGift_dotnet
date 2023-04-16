using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyGift_API.Models.Dto.Update
{
    public class UpdateReviewDTO
    {

        public int Id { get; set; }
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
