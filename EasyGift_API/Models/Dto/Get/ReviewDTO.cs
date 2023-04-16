using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyGift_API.Models.Dto.Get
{
    public class ReviewDTO
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int Rating { get; set; }
        public string ReviewDetail { get; set; }
        public DateTime ReviewDate { get; set; }

    }
}
