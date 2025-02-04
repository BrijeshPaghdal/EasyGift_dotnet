﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyGift_API.Models
{
    public class Address
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Shop")]
        public int ShopId { get; set; }
        
        [MaxLength(200)]
        public string? ShopAddress { get; set; }

        public string? PinCode { get; set; }

        [Required]
        [ForeignKey("Cities")]
        public int CityId { get; set; }
        //changes done by yogesh

    }
}
