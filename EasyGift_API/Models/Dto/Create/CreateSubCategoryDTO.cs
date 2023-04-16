using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyGift_API.Models.Dto.Create
{
    public class CreateSubCategoryDTO
    {
        [Required]
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        [Required]
        [MaxLength(50)]
        public string SubCategoryName { get; set; }
    }
}
