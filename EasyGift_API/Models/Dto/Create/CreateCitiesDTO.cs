﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EasyGift_API.Models.Dto.Create
{
    public class CreateCitiesDTO
    {
        [Required]
        [MaxLength(50)]
        public string CityName { get; set; }
        [Required]
        [ForeignKey("States")]
        public int StateId { get; set; }

    }
}
