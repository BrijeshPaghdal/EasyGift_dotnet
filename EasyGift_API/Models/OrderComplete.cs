using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyGift_API.Models
{
    public class OrderComplete
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderCompleteId { get; set; }

        [ForeignKey("Order")]
        public int Id { get; set; }
        
        public int OrderCompleteStatus { get; set; }

    }
}
