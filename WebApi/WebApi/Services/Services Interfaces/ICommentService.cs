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

        public ServiceResult<IQueryable<CommentDTOOutput>> GetAll(int userId);
        public ServiceResult<CommentDTOOutput> GetById(int commentId, int userId);
        public ServiceResult<IQueryable<int>> GetLikedUsers(int commentId);
        public Task<ServiceResult<bool>> EditCommentAsync(int commentId, int userId, CommentDTOEdit comment);
        public ServiceResult<bool> DeleteComment(int commentId, int userId);
        public Task<ServiceResult<int?>> AddCommentAsync(int userId, CommentDTONew comment);
        public Task<ServiceResult<bool>> EditLikeOnCommentAsync(int commentId, int userId, LikeDTO like);

    }
}
