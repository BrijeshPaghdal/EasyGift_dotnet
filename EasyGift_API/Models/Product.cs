using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyGift_API.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("Shop")]
        public int ShopId { get; set; }
        [ForeignKey("SubCategory")]
        public int SubCategoryId { get; set; }
        public string ProductName { get; set; }
        public string CompanyName { get; set; }
        public int Price { get; set; }
        public int AvailableQuantity { get; set; }
        public string ProductDiscription { get; set; }
        public int ProductStatus { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdateDate { get; set; }

    }
}
