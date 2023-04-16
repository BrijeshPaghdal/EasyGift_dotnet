using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyGift_API.Models.Dto.Get
{
    public class UserOnlineDTO
    {
        public int Id { get; set; }
        public string Session { get; set; }
        public int Time { get; set; }
    }
}
