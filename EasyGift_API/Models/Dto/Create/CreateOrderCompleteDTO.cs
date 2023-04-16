using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyGift_API.Models.Dto.Create
{
    public class CreateOrderCompleteDTO
    {
        [ForeignKey("Order")]
        [Required]
        public int OrderId { get; set; }
        [Required]
        public int OrderCompleteStatus { get; set; }
    }
}
