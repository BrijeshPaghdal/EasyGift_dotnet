using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyGift_API.Models.Dto.Create
{
    public class CreateProductDTO
    {
        [Required]
        [ForeignKey("Shop")]
        public int ShopId { get; set; }
        [Required]
        [ForeignKey("SubCategory")]
        public int SubCategoryId { get; set; }
        [Required]
        [MaxLength(30)]
        public string ProductName { get; set; }
        [Required]
        [MaxLength(30)]
        public string CompanyName { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public int AvailableQuantity { get; set; }
        [Required]
        [MaxLength()]
        public string ProductDiscription { get; set; }
        [Required]
        public int ProductStatus { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }

    }
}
