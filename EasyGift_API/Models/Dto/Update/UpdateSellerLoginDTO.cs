using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyGift_API.Models.Dto.Update
{
    public class UpdateSellerLoginDTO
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string EmailId { get; set; }
        
        [MaxLength(100)]
        [Required]
        public string Password { get; set; }


    }
}
