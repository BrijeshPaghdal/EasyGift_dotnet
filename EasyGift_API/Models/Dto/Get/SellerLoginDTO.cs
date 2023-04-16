using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyGift_API.Models.Dto.Get
{
    public class SellerLoginDTO
    {
        public int Id { get; set; }

        public string EmailId { get; set; }
        
        public string Password { get; set; }


    }
}
