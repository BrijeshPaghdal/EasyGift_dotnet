using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EasyGift_API.Models.Dto.Create
{
    public class CreateNewSellerDTO
    {
        [Required]
        [MaxLength(20)]
        public string SellerName { get; set; }
        [Required]
        [MaxLength(30)]
        public string SellerLastName { get; set; }
        [Required]
        [MaxLength(20)]
        public string SellerPhoneNo { get; set; }
        [Required]
        public string SellerPancardNo { get; set; }
        [Required]
        public string SellerImage { get; set; }
        [Required]
        public string SellerEmail { get;set; }
        [Required]
        public string SellerPassword { get;set; }
    }
}
