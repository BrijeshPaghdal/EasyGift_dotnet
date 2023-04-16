using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyGift_API.Models.Dto.Update
{
    public class UpdateImageDTO
    {

        public int Id { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public string ImageName { get; set; }

    }
}
