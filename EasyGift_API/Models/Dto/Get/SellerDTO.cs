using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyGift_API.Models.Dto.Get
{
    public class SellerDTO
    {
        public int Id { get; set; }
        public string SellerName { get; set; }
        public string SellerLastName { get; set; }
        public string SellerPhoneNo { get; set; }
        public string SellerPancardNo { get; set; }
        public string SellerImage { get; set; }
        public int SellerStatus { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int SellerLoginId { get; set; }

    }
}
