using System.ComponentModel.DataAnnotations;

namespace EasyGift_API.Models.Dto
{
    public class CreateProductDTO
    {
        [Required]
        public int ShopId { get; set; }
        [Required]
        public int SubCategoryId { get; set; }
        [Required]
        [MaxLength(30)]
        public string ProductName { get; set; }
        [Required]
        [MaxLength(30)]
        public string CompanyName { get; set; }
        public int Price { get; set; }
        public int AvailableQuantity { get; set; }
        public string ProductDiscription { get; set; }
        private int ProductStatus { get; set; }
    }
}
