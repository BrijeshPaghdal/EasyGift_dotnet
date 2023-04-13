using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyGift_API.Models
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(20)]
        public string CategoryName { get; set; }
        [Required]
        [MaxLength(100)]
        public string CategoryImageName { get; set; }
       
    }
}
