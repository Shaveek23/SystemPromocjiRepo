﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models.DTO
{
    public class PostDTO : IValidatableObject
    {
        [Required]
        public int? id { get; set; }
        [Required]
        [MaxLength(50)]
        public string title { get; set; }
        [Required]
        public string content { get; set; }
        [Required]
        public DateTime? datetime { get; set; }
        [Required]
        public int? category { get; set; }
        [Required]
        public bool? isPromoted { get; set; }
        [Required]
        public string author { get; set; }
        [Required]
        public int? authorID { get; set; }
        [Required]
        public int? likesCount { get; set; }
        [Required]
        public bool? isLikedByUser { get; set; }


        //[Required]
        //public IQueryable<CommentDTO> comments { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (content == "" && title == "")
                yield return new ValidationResult("If content is empty, post must contain some title.", new[] { nameof(PostDTO) });
        }


    }
}