using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyGift_API.Models
{
    public class Admin
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AdminId { get; set; }

        [Required]
        [MaxLength(20)]
        public string AdminName { get; set; }
        
        [MaxLength(20)]
        [Required]
        public string AdminEmail { get; set; }

        [Required]
        [MaxLength(200)]
        public string AdminPassword { get; set; }


    }
}
