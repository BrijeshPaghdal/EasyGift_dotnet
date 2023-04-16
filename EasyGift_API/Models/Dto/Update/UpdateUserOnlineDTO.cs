using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyGift_API.Models.Dto.Update
{
    public class UpdateUserOnlineDTO
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Session { get; set; }
        [Required]
        public int Time { get; set; }
    }
}
