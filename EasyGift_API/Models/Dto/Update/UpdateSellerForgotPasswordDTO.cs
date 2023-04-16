using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyGift_API.Models.Dto.Update
{
    public class UpdateSellerForgotPasswordDTO
    {
        [Required]
        public int Id { get; set; }
        [ForeignKey("SellerLogin")]
        public int SellerLoginId { get; set; }
        public DateTime Validtill { get; set; }

    }
}
