﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyGift_API.Models.Dto.Create
{
    public class CreateProductSuggestionDTO
    {
        [ForeignKey("Product")]
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int SuggestionId { get; set; }
    }
}
