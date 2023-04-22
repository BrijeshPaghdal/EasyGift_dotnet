using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyGift_API.Models.Dto.Get
{
    public class NewSellerDTO
    {
        //public int SellerLoginId { get; set; }
        public string SellerName { get; set; }
        public string SellerLastName { get; set; }
        public string? ShopName { get; set; } = null;
        public string EmailId { get; set; }
        public string SellerImage { get; set; }
        public int SellerStatus { get; set; }
        public int SellerLoginId { get; set; }
      
    }
}
