using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyGift_API.Models.Dto.Update
{
    public class UpdateProductSuggestionDTO
    {
        [Required]
        public int Id { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public int SuggestionId { get; set; }
    }
}
