using System.ComponentModel.DataAnnotations;

namespace EasyGift_API.Models.Dto.Create
{
    public class CreateCountriesDTO
    {
        [Required]
        [MaxLength(20)]
        public string SortName { get; set; }
        [Required]
        [MaxLength(50)]
        public string CountryName { get; set; }
        [Required]
        public int PhoneCode { get; set; }
    }
}
