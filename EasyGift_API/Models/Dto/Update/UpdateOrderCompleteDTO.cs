using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyGift_API.Models.Dto.Update
{
    public class UpdateOrderCompleteDTO
    {
        [Required]
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int OrderCompleteStatus { get; set; }
    }
}
