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
        public IQueryable<CommentDTO> GetAll(int userId);
        public CommentDTO GetById(int commentId,int userId);
        public IQueryable<int> GetLikedUsers(int commentId);
        public Task<CommentDTO> EditCommentAsync(int commentId,int userId, CommentDTO comment);
        public bool DeleteComment(int  commentId,int userId);
        public Task<CommentDTO> AddCommentAsync(int userId,CommentDTO comment);
        public Task<bool> EditLikeOnCommentAsync(int commentId,int userId);
    }
}
