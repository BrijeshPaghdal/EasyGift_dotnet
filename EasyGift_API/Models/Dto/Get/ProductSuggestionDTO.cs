using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyGift_API.Models.Dto.Get
{
    public class ProductSuggestionDTO
    {
        public int Id { get; set; }

        public int ProductId { get; set; }
        
        public int SuggestionId { get; set; }

    }
}
