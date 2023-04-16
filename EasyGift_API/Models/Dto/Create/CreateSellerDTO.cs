using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyGift_API.Models.Dto.Create
{
    public class CreateSellerDTO
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
        [MaxLength(20)]
        public string SellerPancardNo { get; set; }
        [Required]
        [MaxLength(100)]
        public string SellerImage { get; set; }
        [Required]
        public int SellerStatus { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        [ForeignKey("SellerLogin")]
        [Required]
        public int SellerLoginId { get; set; }

    }
}
