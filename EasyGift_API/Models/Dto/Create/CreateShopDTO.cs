using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyGift_API.Models.Dto.Create
{
    public class CreateShopDTO
    {
        [ForeignKey("Seller")]
        [Required]
        public int SellerId { get; set; }
        [MaxLength(50)]
        public string ShopName { get; set; }
        [MaxLength(20)]
        public string GSTNo{ get; set; }
        [MaxLength(50)]
        public string Latitude{ get; set; }
        [MaxLength(50)]
        public string Longitude { get; set; }
    }
}
