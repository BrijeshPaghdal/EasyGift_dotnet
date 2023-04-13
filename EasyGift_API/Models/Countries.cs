using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyGift_API.Models
{
    public class Countries
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
