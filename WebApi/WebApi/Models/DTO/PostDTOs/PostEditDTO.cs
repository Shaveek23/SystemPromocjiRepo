﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models.DTO.PostDTOs
{
    //DTO for creating and editing post
    public class PostEditDTO : IValidatableObject
    {
        [Required]
        [MaxLength(50)]
        public string title { get; set; }
        [Required]
        public string content { get; set; }
        [Required]
        public int category { get; set; }
        [Required]
        public bool isPromoted { get; set; }
        [Required]
        public DateTime dateTime { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            yield return null;
        }
    }
}