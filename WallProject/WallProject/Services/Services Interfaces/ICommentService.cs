using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WallProject.Models;

namespace WallProject.Services.Services_Interfaces
{
    public interface ICommentService
    {
        public Task<ServiceResult<CommentViewModel>> getById(int commentID, int userID);
        public Task<ServiceResult<List<CommentViewModel>>> getAll(int userID);
        public Task<ServiceResult<List<CommentViewModel>>> getByPostId(int postID, int userID);
        public Task<ServiceResult<List<CommentViewModel>>> getByUserId(int userID);
        public Task<ServiceResult<int?>> getCommentLikes(int commentID);
        public Task<ServiceResult<bool>> AddNewComment(string commentText, int postId,int userId);
        public Task<ServiceResult<bool>> EditLikeStatus(int commentID, int userID, bool like);
        public Task<ServiceResult<bool>> Edit(int userID, int commentID, string content);
    }
}
