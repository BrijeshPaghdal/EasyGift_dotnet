using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyGift_API.Models.Dto.Update
{
    public class UpdateSubCategoryDTO
    {
        [Required]
        public int Id { get; set; }
        public int CategoryId { get; set; }
        [MaxLength(50)]
        public string SubCategoryName { get; set; }
    }
}
