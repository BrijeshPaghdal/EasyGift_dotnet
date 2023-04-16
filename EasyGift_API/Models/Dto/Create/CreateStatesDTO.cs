using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyGift_API.Models.Dto.Create
{
    public class CreateStatesDTO
    {
        [Required]
        [MaxLength(50)]
        public string StateName { get; set; }
        [Required]
        [ForeignKey("Countries")]
        public int CountryId { get; set; }
       
    }
}
