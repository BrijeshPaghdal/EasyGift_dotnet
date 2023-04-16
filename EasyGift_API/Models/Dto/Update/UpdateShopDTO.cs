using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyGift_API.Models.Dto.Update
{
    public class UpdateShopDTO
    {
        [Required]
        public int Id { get; set; }
        [ForeignKey("Seller")]
        public int SellerId { get; set; }
        [MaxLength(50)]
        public string ShopName { get; set; }
        [MaxLength(20)]
        public string GSTNo { get; set; }
        [MaxLength(50)]
        public string Latitude { get; set; }
        [MaxLength(50)]
        public string Longitude { get; set; }
    }
}
