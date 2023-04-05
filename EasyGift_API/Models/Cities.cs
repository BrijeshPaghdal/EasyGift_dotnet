using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyGift_API.Models
{
    public class Cities
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CityId { get; set; }
        [Required]
        [MaxLength(50)]
        public string CityName { get; set; }
        [Required]
        [ForeignKey("States")]
        public int StateId { get; set; }
       
    }
}
