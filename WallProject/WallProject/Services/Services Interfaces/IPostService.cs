﻿using System;
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
        public Task<ServiceResult<List<PostViewModel>>> getUserPosts(int userID);
        public Task<ServiceResult<bool>> AddNewPost(string postText, int userId, int categoryId, string title);
        public Task<ServiceResult<bool>> EditPost(int userID, int postID, string title, string content, int categoryID, bool isPromoted);
        public Task<ServiceResult<bool>> DeletePost(int userID, int postID);
        public Task<ServiceResult<bool>> EditLikeStatus(int postID, int userID, bool like);
    }
}
