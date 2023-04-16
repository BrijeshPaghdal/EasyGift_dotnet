using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyGift_API.Models.Dto.Update
{
    public class UpdateStatesDTO
    {
        [Required]
        public int Id { get; set; }
        [MaxLength(50)]
        public string StateName { get; set; }
        [ForeignKey("Countries")]
        public int CountryId { get; set; }
       
    }
}
