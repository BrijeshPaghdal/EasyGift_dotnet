using System.ComponentModel.DataAnnotations;

namespace EasyGift_API.Models.Dto.Update
{
    public class UpdateCustomerLoginDTO
    {
        public int Id { get; set; }
        public string EmailId { get; set; }

        [MaxLength(100)]
        [Required]
        public string Password { get; set; }
    }
}
