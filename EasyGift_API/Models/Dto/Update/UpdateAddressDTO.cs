using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyGift_API.Models.Dto.Update
{
    public class UpdateAddressDTO
    {
        public int AddressId { get; set; }
        public int ShopId { get; set; }

        [MaxLength(200)]
        public string? ShopAddress { get; set; }

        [Required]
        public int? PinCode { get; set; }

        [ForeignKey("Cities")]
        public int CityId { get; set; }

    }
}
