using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyGift_API.Models.Dto.Update
{
    public class UpdateSellerDTO
    {
        [Required]
        public int Id { get; set; }
        
        [MaxLength(20)]
        public string SellerName { get; set; }
        
        public string SellerLastName { get; set; }
        
        [MaxLength(20)]
        public string SellerPhoneNo { get; set; }
        
        public string SellerPancardNo { get; set; }
        
        public string SellerImage { get; set; }
        
        public int SellerStatus { get; set; }
        
        public DateTime CreatedDate { get; set; }
        [ForeignKey("SellerLogin")]
        
        public int SellerLoginId { get; set; }

    }
}
