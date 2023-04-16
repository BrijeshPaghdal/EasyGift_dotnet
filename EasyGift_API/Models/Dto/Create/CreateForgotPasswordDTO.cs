using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyGift_API.Models.Dto.Create
{
    public class CreateForgotPasswordDTO
    {

        [Required]
        [ForeignKey("CustomerLoginId")]
        public int CustomerLoginId { get; set; }
        
        [Required]
        public DateTime Validtill { get; set; }

    }
}
