﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EasyGift_API.Controllers;

namespace EasyGift_API.Models.Dto.Update
{
    public class UpdateSellerDTO
    {
        [Required]
        public int Id { get; set; }
        
        [MaxLength(20)]
        public string SellerName { get; set; }
        [MaxLength(30)]
        public string SellerLastName { get; set; }
        
        [MaxLength(20)]
        public string SellerPhoneNo { get; set; }
        [MaxLength(20)]
        public string SellerPancardNo { get; set; }
        [MaxLength(100)]
        public string SellerImage { get; set; }

        public DateTime CreatedDate { get; set; }
        public int SellerStatus { get; set; }
        //public DateTime UpdateDate { get; set; }
        [ForeignKey("SellerLogin")]
        public int SellerLoginId { get; set; }

    }
}
