using System.ComponentModel.DataAnnotations;

namespace EasyGift_API.Models.Dto.Create
{
    public class CreateCustomerLoginDTO
    {
        [Required]
        [MaxLength(200)]
        public string EmailId { get; set; }

        [MaxLength(100)]
        [Required]
        public string Password { get; set; }
    }
}
