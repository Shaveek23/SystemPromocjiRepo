using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WallProject.Models;

namespace WallProject.Services.Services_Interfaces
{
    public interface ICommentService
    {
        public Task<CommentViewModel> getById(int commentID, int userID);
        public Task<List<CommentViewModel>> getAll(int userID);
        public Task<List<CommentViewModel>> getByPostId(int postID, int userID);
        public Task<int> getCommentLikes(int commentID);
    }
}
