using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyGift_API.Models.Dto.Update
{
    public class UpdateSellerBankDetailsDTO
    {
        [Required]
        public int Id { get; set; }
        public int SellerId { get; set; }
        [ForeignKey("BankDetails")]
        public int BankId { get; set; }
        public string BankAccountNo { get; set; }

    }
}
