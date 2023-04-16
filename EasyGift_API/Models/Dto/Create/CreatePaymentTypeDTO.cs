using System.ComponentModel.DataAnnotations;

namespace EasyGift_API.Models.Dto.Create
{
    public class CreatePaymentTypeDTO
    {
        [Required]
        public string PaymentMethod { get; set; }
    }
}
