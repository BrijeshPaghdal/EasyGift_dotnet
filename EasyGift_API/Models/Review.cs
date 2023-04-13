using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyGift_API.Models
{
    public class Review
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public int Rating { get; set; }
        public string ReviewDetail { get; set; }
        public DateTime ReviewDate { get; set; }

    }
}
