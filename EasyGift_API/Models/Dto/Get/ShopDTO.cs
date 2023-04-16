using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyGift_API.Models.Dto.Get
{
    public class ShopDTO
    {
        public int Id { get; set; }
        public int SellerId { get; set; }
        public string ShopName { get; set; }
        public string GSTNo{ get; set; }
        public string Latitude{ get; set; }
        public string Longitude { get; set; }




    }
}
