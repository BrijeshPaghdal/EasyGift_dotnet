using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyGift_API.Models.Dto.Update
{
    public class UpdateSuggestionDTO
    {
        [Required]
        public int Id { get; set; }
        
        public string SuggestedFor { get; set; }
        
        public string Gender { get; set; }
        
        public int MinAge { get; set; }
        
        public int MaxAge { get; set; }
    }
}
