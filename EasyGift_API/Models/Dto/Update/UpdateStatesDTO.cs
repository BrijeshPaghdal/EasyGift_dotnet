using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyGift_API.Models.Dto.Update
{
    public class UpdateStatesDTO
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string StateName { get; set; }
        [Required]
        [ForeignKey("Countries")]
        public int CountryId { get; set; }
       
    }
}
