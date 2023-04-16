using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyGift_API.Models.Dto.Create
{
    public class CreateSuggestionDTO
    {
        [Required]
        public string SuggestedFor { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public int MinAge { get; set; }
        [Required]
        public int MaxAge { get; set; }
    }
}
