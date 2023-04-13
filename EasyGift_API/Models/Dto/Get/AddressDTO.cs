using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyGift_API.Models.Dto.Get
{
    public class AddressDTO
    {
        public int Id { get; set; }
        public int ShopId { get; set; }
        public string? ShopAddress { get; set; }
        public int? PinCode { get; set; }
        public int CityId { get; set; }

    }
}
