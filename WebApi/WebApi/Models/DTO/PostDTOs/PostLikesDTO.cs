using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models.DTO.PostDTOs
{
    public class PostLikesDTO
    {
        public IQueryable<int> likers { get; set; }
    }
}
