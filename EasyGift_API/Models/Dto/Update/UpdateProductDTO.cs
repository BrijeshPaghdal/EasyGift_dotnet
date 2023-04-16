using System.ComponentModel.DataAnnotations;

namespace EasyGift_API.Models.Dto.Update
{
    public class UpdateProductDTO
    {
        [Required]
        public int Id { get; set; }
        public int ShopId { get; set; }
        public int SubCategoryId { get; set; }
        [MaxLength(30)]
        public string ProductName { get; set; }
        [MaxLength(30)]
        public string CompanyName { get; set; }
        public int Price { get; set; }
        public int AvailableQuantity { get; set; }
        public string ProductDiscription { get; set; }
        public int ProductStatus { get; set; }
        private DateTime UpdateDate { get; set; }

    }
}
