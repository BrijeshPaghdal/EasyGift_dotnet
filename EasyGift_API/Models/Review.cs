using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyGift_API.Models
{
    public class Review
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReviewId { get; set; }

        [ForeignKey("Oder")]
        public int Id { get; set; }
        public int Rating { get; set; }
        public string ReviewDetail { get; set; }
        public DateTime ReviewDate { get; set; }

    }
}
