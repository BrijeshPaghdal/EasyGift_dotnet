using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyGift_API.Models.Dto.Create
{
    public class CreateAddressDTO
    {
        [Required]
        [ForeignKey("Shop")]
        public int ShopId { get; set; }

        [MaxLength(200)]
        public string? ShopAddress { get; set; }

        [Required]
        public int? PinCode { get; set; }

        [Required]
        [ForeignKey("Cities")]
        public int CityId { get; set; }

    }
}
