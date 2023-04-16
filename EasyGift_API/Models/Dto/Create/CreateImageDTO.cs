using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyGift_API.Models.Dto.Create
{
    public class CreateImageDTO
    {
        
        [ForeignKey("Product")]
        [Required]

        public int ProductId { get; set; }
        [Required]
        public string ImageName { get; set; }

    }
}
