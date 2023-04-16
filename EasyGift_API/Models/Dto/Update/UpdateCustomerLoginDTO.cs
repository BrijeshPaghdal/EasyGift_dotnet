using System.ComponentModel.DataAnnotations;

namespace EasyGift_API.Models.Dto.Update
{
    public class UpdateCustomerLoginDTO
    {
        [Required]
        public int Id { get; set; }
        [MaxLength(200)]
        public string EmailId { get; set; }

        [MaxLength(100)]
        public string Password { get; set; }
    }
}
