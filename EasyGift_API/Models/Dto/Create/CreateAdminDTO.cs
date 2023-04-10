using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyGift_API.Models.Dto.Create
{
    public class CreateAdminDTO
    {

        [Required]
        [MaxLength(20)]
        public string AdminName { get; set; }

        [Required]
        [MaxLength(20)]
        public string AdminEmail { get; set; }

        [Required]
        [MaxLength(200)]
        public string AdminPassword { get; set; }

    }
}
