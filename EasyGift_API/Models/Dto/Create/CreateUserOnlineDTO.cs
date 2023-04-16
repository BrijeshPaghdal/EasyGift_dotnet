﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyGift_API.Models.Dto.Create
{
    public class CreateUserOnlineDTO
    {
        [Required]
        public string Session { get; set; }
        [Required]
        public int Time { get; set; }
    }
}
