using System.ComponentModel.DataAnnotations;

namespace EasyGift_API.Models.Dto.Update
{
    public class UpdatePaymentTypeDTO
    {
        public int Id { get; set; }
        [Required]
        public string PaymentMethod { get; set; }
    }
}
