using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyGift_API.Models
{
    public class Seller
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(20)]
        public string SellerName { get; set; }
        public string SellerLastName { get; set; }
        [Required]
        [MaxLength(20)]
        public string SellerPhoneNo { get; set; }
        public string SellerPancardNo { get; set; }
        public string SellerImage { get; set; }
        public int SellerStatus { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        [ForeignKey("SellerLogin")]
        public int SellerLoginId { get; set; }

    }
}
