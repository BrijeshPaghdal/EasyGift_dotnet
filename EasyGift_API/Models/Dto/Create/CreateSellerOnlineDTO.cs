using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyGift_API.Models.Dto.Create
{
    public class CreateSellerOnlineDTO
    {
        [Required]
        [MaxLength(100)]
        public string Session { get; set; }
        [Required]
        public int Time { get; set; }


    }
}
