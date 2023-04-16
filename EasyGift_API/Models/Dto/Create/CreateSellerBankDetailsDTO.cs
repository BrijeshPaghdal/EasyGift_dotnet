using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyGift_API.Models.Dto.Create
{
    public class CreateSellerBankDetailsDTO
    {
        [ForeignKey("Seller")]
        [Required]
        public int SellerId { get; set; }
        [ForeignKey("BankDetails")]
        [Required]
        public int BankId { get; set; }
        public string BankAccountNo { get; set; }

    }
}
