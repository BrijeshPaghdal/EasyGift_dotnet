using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyGift_API.Models
{
    public class Review
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReviewId { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        
        public int SuggestionId { get; set; }

    }
}
