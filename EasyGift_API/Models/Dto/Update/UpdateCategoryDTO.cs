using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyGift_API.Models.Dto.Update
{
    public class CategoryDTO
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string CategoryName { get; set; }

        [Required]
        [MaxLength(100)]
        public string CategoryImageName { get; set; }

    }
}
