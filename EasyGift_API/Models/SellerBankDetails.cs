using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyGift_API.Models
{
    public class SellerBankDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SellerBankId { get; set; }

        [ForeignKey("Seller")]
        public int SellerId { get; set; }
        [ForeignKey("BankDetails")]
        public int BankId { get; set; }
        public string BankAccountNo { get; set; }

    }
}
