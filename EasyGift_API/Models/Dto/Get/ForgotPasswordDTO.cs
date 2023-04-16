using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyGift_API.Models.Dto.Get
{
    public class ForgotPasswordDTO
    {
        public int Id { get; set; }

        public int CustomerLoginId { get; set; }
        
        public DateTime Validtill { get; set; }

    }
}
