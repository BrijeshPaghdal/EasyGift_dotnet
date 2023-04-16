using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyGift_API.Models.Dto.Get   
{
    public class SellerForgotPasswordDTO
    {
        public int Id { get; set; }

        public int SellerLoginId { get; set; }
        
        public DateTime Validtill { get; set; }

    }
}
