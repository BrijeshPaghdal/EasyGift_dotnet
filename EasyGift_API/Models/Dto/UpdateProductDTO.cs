using System.ComponentModel.DataAnnotations;

namespace EasyGift_API.Models.Dto
{
    public class UpdateProductDTO
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
        [Required]
        public int Price { get; set; }
        [Required]
        public int AvailableQuantity { get; set; }
        [Required]
        public string ProductDiscription { get; set; }
        private DateTime UpdateDate { get => UpdateDate; set => UpdateDate = DateTime.Now; }

    }
}
