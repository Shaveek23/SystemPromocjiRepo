using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WallProject.Models;

namespace WallProject.Services.Services_Interfaces
{
    public interface IPostService
    {
        public Task<ServiceResult<PostViewModel>> getById(int postID, int userID);
        public Task<ServiceResult<List<PostViewModel>>> getAll(int userID);
        public Task<ServiceResult<bool>> AddNewPost(string postText, int userId);
    }
}
