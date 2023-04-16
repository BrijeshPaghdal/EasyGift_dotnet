using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyGift_API.Models.Dto.Update
{
    public class UpdateSuggestionDTO
    {
        public int Id { get; set; }
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
