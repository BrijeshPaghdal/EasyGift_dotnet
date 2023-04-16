using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EasyGift_API.Models.Dto.Update
{
    public class UpdateCitiesDTO
    {
        [Required]
        public int Id { get; set; }
        
        [MaxLength(50)]
        public string CityName { get; set; }
        
        [ForeignKey("States")]
        public int StateId { get; set; }

    }
}
