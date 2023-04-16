using System.ComponentModel.DataAnnotations;

namespace EasyGift_API.Models.Dto.Create
{
    public class CreatePaymentTypeDTO
    {
        [Required]
        [MaxLength(30)]
        public string PaymentMethod { get; set; }
    }
}
