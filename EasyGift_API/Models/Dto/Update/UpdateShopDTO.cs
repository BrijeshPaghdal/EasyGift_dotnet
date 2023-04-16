using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyGift_API.Models.Dto.Update
{
    public class UpdateShopDTO
    {
        public int Id { get; set; }
        [ForeignKey("Seller")]
        [Required]
        public int SellerId { get; set; }
        public string ShopName { get; set; }
        public string GSTNo{ get; set; }
        public string Latitude{ get; set; }
        public string Longitude { get; set; }
    }
}
