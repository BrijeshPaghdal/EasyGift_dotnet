using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyGift_API.Models
{
    public class Shop
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("Seller")]
        public int SellerId { get; set; }
        public string? ShopName { get; set; }
        public string? GSTNo{ get; set; }
        public string? Latitude{ get; set; }
        public string? Longitude { get; set; }




    }
}
