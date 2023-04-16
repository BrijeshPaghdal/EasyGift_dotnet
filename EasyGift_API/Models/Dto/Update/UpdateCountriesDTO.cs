using System.ComponentModel.DataAnnotations;

namespace EasyGift_API.Models.Dto.Update
{
    public class UpdateCountriesDTO
    {
        [Required]
        public int Id { get; set; }
        
        [MaxLength(20)]
        public string SortName { get; set; }
        
        [MaxLength(50)]
        public string CountryName { get; set; }
        
        public int PhoneCode { get; set; }
    }
}
