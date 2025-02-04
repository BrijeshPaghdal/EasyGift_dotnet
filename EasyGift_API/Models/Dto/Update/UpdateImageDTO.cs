﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyGift_API.Models.Dto.Update
{
    public class UpdateImageDTO
    {
        [Required]
        public int Id { get; set; }

        public int ProductId { get; set; }
        [MaxLength(200)]
        public string ImageName { get; set; }

    }
}
