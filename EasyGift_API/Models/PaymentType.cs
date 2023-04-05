using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyGift_API.Models
{
    public class PaymentType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PaymentId { get; set; }

        public string PaymentMethod { get; set; }

    }
}
