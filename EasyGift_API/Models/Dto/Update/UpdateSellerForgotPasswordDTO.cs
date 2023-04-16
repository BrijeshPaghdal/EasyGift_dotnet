using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyGift_API.Models.Dto.Update
{
    public class UpdateSellerForgotPasswordDTO
    {
        public int Id { get; set; }
        [Required]
        [ForeignKey("SellerLogin")]
        public int SellerLoginId { get; set; }
        
        [Required]
        public DateTime Validtill { get; set; }

    }
}
