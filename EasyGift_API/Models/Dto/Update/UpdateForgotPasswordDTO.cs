using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyGift_API.Models.Dto.Update
{
    public class UpdateForgotPasswordDTO
    {

        public int Id { get; set; }
        [Required]
        [ForeignKey("CustomerLoginId")]
        public int CustomerLoginId { get; set; }
        
        [Required]
        public DateTime Validtill { get; set; }

    }
}
