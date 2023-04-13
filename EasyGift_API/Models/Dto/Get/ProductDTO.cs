using System.ComponentModel.DataAnnotations;

namespace EasyGift_API.Models.Dto.Get
{
    public class ProductDTO
    {
        public int ProductId { get; set; }
        public int ShopId { get; set; }
        public int SubCategoryId { get; set; }
        public string? ProductName { get; set; }
        public string? CompanyName { get; set; }
        public int Price { get; set; }
        public int AvailableQuantity { get; set; }
        public string? ProductDiscription { get; set; }
        public int ProductStatus { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
