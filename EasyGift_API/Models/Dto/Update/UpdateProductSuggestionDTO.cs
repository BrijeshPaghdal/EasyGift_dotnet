using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyGift_API.Models.Dto.Update
{
    public class UpdateProductSuggestionDTO
    {
        public int Id { get; set; }
        [ForeignKey("Product")]
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int SuggestionId { get; set; }
    }
}
