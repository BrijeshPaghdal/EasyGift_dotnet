﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyGift_API.Models.Dto.Update
{
    public class UpdateCategoryDTO
    {
        public int CategoryId { get; set; }

        [MaxLength(20)]
        public string CategoryName { get; set; }

        [MaxLength(100)]
        public string CategoryImageName { get; set; }

    }
}
