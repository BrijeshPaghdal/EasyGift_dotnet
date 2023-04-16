using System.ComponentModel.DataAnnotations;

namespace EasyGift_API.Models.Dto.Update
{
    public class UpdateCountriesDTO
    {
        public int Id { get; set; }
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
