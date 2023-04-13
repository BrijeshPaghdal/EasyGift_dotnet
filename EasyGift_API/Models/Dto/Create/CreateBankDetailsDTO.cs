using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyGift_API.Models.Dto.Create
{
    public class CreateBankDetailsDTO
    {
        [MaxLength(49)]
        public string? BankName { get; set; }
        [MaxLength(11)]
        public string? BankIFSC { get; set; }
        [MaxLength(74)]
        public string? BankBranch { get; set; }
        [MaxLength(195)]
        public string? BankAddress { get; set; }
        [MaxLength(50)]
        public string? BankCity { get; set; }
        [MaxLength(50)]
        public string? BankDistrict { get; set; }
        [MaxLength(26)]
        public string? BankState { get; set; }
        [MaxLength(50)]
        public string? BankCountry { get; set; }
    }
}
