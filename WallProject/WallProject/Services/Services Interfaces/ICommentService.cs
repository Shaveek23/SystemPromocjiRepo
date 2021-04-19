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
        public Task<ServiceResult<int?>> getCommentLikes(int commentID);
        public  Task AddNewComment(string commentText, int postId,int userId);
    }
}
