using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyGift_API.Models.Dto.Get
{
    public class BankDetailsDTO
    {
        public int Id { get; set; }
        public string? BankName { get; set; }
        public string? BankIFSC { get; set; }
        public string? BankBranch { get; set; }
        public string? BankAddress { get; set; }
        public string? BankCity { get; set; }
        public string? BankDistrict { get; set; }
        public string? BankState { get; set; }
        public string? BankCountry { get; set; }
    }
}
