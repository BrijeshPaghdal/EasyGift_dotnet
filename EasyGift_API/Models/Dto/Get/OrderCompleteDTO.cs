using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyGift_API.Models.Dto.Get
{
    public class OrderCompleteDTO
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public int OrderCompleteStatus { get; set; }

    }
}
