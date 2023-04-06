using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyGift_API.Models
{
    public class States
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StateId { get; set; }
        [Required]
        [MaxLength(50)]
        public string StateName { get; set; }
        [Required]
        [ForeignKey("Countries")]
        public int CountryId { get; set; }
       
    }
}
