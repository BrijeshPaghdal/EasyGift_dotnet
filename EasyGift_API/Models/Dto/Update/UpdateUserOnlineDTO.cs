using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyGift_API.Models.Dto.Update
{
    public class UpdateUserOnlineDTO
    {
        [Required]
        public int Id { get; set; }
        public string Session { get; set; }
        
        public int Time { get; set; }
    }
}
