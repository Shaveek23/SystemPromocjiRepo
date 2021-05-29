using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WallProject.Models;

namespace WallProject.Services.Services_Interfaces
{
    public interface IWallService
    {
        public Task<ServiceResult<WallViewModel>> getWall(int userID);

        public Task<ServiceResult<WallViewModel>> getUserPosts(int userID);

        public Task<ServiceResult<CommentListViewModel>> getUserComments(int userID);

        public Task<ServiceResult<PostEditViewModel>> getPostToEdit(int userID, int postID);

        public void ChangeCategoryFilterStatus(int categoryID);

    }
}
