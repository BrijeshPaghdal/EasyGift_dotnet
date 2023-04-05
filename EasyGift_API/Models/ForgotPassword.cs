﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyGift_API.Models
{
    public class ForgotPassword
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ForgotPassowrdId { get; set; }

        [Required]
        [ForeignKey("CustomerLoginId")]
        public int CustomerLoginId { get; set; }
        
        [Required]
        public DateTime Validtill { get; set; }

    }
}
