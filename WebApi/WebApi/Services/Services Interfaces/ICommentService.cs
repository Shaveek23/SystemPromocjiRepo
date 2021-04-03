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
        public IQueryable<CommentDTOOutput> GetAll(int userId);
        public CommentDTOOutput GetById(int commentId,int userId);
        public IQueryable<int> GetLikedUsers(int commentId);
        public Task<CommentDTOOutput> EditCommentAsync(int commentId,int userId, CommentDTO comment);
        public bool DeleteComment(int  commentId,int userId);
        public Task<CommentDTOOutput> AddCommentAsync(int userId,CommentDTO comment);
        public Task<bool> EditLikeOnCommentAsync(int commentId,int userId);
    }
}
