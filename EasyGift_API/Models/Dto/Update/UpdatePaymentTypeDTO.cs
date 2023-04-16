using System.ComponentModel.DataAnnotations;

namespace EasyGift_API.Models.Dto.Update
{
    public class UpdatePaymentTypeDTO
    {
        [Required]
        public int Id { get; set; 
        [MaxLength(30)]
        public string PaymentMethod { get; set; }
    }
}
