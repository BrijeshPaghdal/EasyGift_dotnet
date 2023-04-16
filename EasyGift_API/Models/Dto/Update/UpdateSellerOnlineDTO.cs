using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyGift_API.Models.Dto.Update
{
    public class UpdateSellerOnlineDTO
    {
        [Required]
        public int Id { get; set; }
        [MaxLength(100)]
        public string Session { get; set; }
        public int Time { get; set; }


    }
}
