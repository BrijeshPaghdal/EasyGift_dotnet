using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyGift_API.Models.Dto.Create
{
    public class CreateSellerForgotPasswordDTO
    {
        [Required]
        [ForeignKey("SellerLogin")]
        public int SellerLoginId { get; set; }
        
        [Required]
        public DateTime Validtill { get; set; }

    }
}
