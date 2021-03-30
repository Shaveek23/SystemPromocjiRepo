using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models.DTO;

namespace WebApi.Services.Services_Interfaces
{
    public interface IPostService
    {
        public IQueryable<PostDTO> GetAll();

        public PostDTO GetById(int id);
        public Task<PostDTO> AddPersonAsync(PostDTO newPostDTO);
    }
}
