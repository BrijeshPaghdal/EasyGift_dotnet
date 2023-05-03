using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyGift_API.Models
{
    public class SellerLogin
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string EmailId { get; set; }

        [MaxLength(100)]
        [Required]
        private string Password;

        public string password { get { return Password; } set { Password = value; } }


    }
}
