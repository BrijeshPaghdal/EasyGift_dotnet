using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyGift_API.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }
        [Required]
        [ForeignKey("Shop")]
        public int ShopId { get; set; }
        [Required]
        [ForeignKey("SubCategory")]
        public int SubCategoryId { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public string CompanyName { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public int AvailableQuantity { get; set; }
        public string ProductDiscription { get; set; }
        public int ProductStatus { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdateDate { get; set; }


    }
}
