using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyGift_API.Models.Dto.Update
{
    public class UpdateSellerLoginDTO
    {
        [Required]
        public int Id { get; set; }
        [MaxLength(100)]
        public string EmailId { get; set; }
        
        [MaxLength(500)]
        public string Password { get; set; }


    }
}
