using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyGift_API.Models.Dto.Update
{
    public class UpdateForgotPasswordDTO
    {

        [Required]
        public int Id { get; set; }
        
        [ForeignKey("CustomerLoginId")]
        public int CustomerLoginId { get; set; }
        
        
        public DateTime ValidTill { get; set; }

    }
}
