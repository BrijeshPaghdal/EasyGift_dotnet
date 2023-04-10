using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyGift_API.Models.Dto.Update
{
    public class UpdateAdminDTO
    {
        public int AdminId { get; set; }

        [MaxLength(20)]
        public string AdminName { get; set; }

        [MaxLength(20)]
        public string AdminEmail { get; set; }

        [MaxLength(200)]
        public string AdminPassword { get; set; }

    }
}
