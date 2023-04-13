using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyGift_API.Models.Dto.Update
{
    public class UpdateAdminDTO
    {
        [Required]
        public int Id { get; set; }

        [MaxLength(20)]
        public string AdminName { get; set; }

        [MaxLength(20)]
        public string AdminEmail { get; set; }

        [MaxLength(200)]
        public string AdminPassword { get; set; }

    }
}
