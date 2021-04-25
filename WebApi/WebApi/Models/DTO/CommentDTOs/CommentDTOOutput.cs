using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models.DTO
{
    public class CommentDTOOutput
    {
        [Required]
        public int? id { get; set; }


        [Required]
        public bool? ownerMode { get; set; }

        [Required]
        [MaxLength(255)]
        [MinLength(1)]
        public string? content { get; set; }

        [Required]
        public DateTime? date { get; set; }

        [Required]
        public int? authorID { get; set; }

        [Required]
        public string? authorName { get; set; }

        [Required]
        public int? likesCount { get; set; }
        [Required]
        public bool? isLikedByUser { get; set; }


        public int? postId { get; set; }
    }
}
