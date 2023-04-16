using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyGift_API.Models.Dto.Get
{
    public class ImageDTO
    {
        public int Id { get; set; }

        public int ProductId { get; set; }
        
        public string ImageName { get; set; }

    }
}
