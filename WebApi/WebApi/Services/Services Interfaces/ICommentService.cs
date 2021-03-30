using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using WebApi.Models.DTO;

namespace WebApi.Services.Services_Interfaces
{
    public interface ICommentService
    {
        public IQueryable<CommentDTO> GetAll();
        public CommentDTO GetById(int commentId);
        public IQueryable<int> GetLikedUsers(int commentId);
        public Task<CommentDTO> EditCommentAsync( CommentDTO comment);
        public void DeleteComment(int  commentId);
        public Task<CommentDTO> AddCommentAsync(CommentDTO comment);
        public Task EditLikeOnCommentAsync(int commentId);
    }
}
