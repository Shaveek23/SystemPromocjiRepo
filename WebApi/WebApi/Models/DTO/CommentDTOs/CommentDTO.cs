﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models.DTO
{
    public class CommentDTO
    {
        
        [Required]
        public int? UserID { get; set; }

        [Required]
        public int? PostID { get; set; }

        [Required]
        public DateTime? DateTime { get; set; }

        [Required]
        [MaxLength(255)]
        [MinLength(1)]
        public string Content { get; set; }

    }
}