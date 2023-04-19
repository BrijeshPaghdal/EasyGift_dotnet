using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyGift_API.Models.Dto.Get
{
    public class SellerBankDetailsDTO
    {
        public int Id { get; set; }
        public int SellerId { get; set; }
        public int BankId { get; set; }
        public string? BankAccountNo { get; set; }

    }
}
