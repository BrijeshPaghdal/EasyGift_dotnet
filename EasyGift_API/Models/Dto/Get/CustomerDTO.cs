using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyGift_API.Models.Dto.Get
{
    public class CustomerDTO
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string MobileNo { get; set; }
        public int CustomerLoginId { get; set; }
        public int CustomerStatus { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdateDate { get; set; }

    }
}
