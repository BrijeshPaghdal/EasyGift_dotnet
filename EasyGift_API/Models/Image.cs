using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyGift_API.Models
{
    public class Image
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ImageId { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        
        public string ImageName { get; set; }

    }
}
