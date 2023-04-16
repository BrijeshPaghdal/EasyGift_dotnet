using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyGift_API.Models.Dto.Update
{
    public class UpdateSellerBankDetailsDTO
    {
        public int Id { get; set; }
        public int SellerId { get; set; }
        [ForeignKey("BankDetails")]
        [Required]
        public int BankId { get; set; }
        [MaxLength(50)]
        public string BankAccountNo { get; set; }

    }
}
