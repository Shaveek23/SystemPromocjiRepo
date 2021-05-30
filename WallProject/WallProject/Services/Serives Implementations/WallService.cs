using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WallProject.Models;
using WallProject.Services.Services_Interfaces;
using Newtonsoft.Json;

namespace WallProject.Services.Serives_Implementations
{
    public class WallService : IWallService
    {
        
        private readonly IPostService _postService;
        private readonly ICommentService _commentService;
        private readonly IUserService _userService;
        private readonly ICategoryService _categoryService;
        public WallService(IPostService postService, IUserService userService, ICategoryService categoryService, ICommentService commentService)
        {
            _postService = postService;
            _userService = userService;
            _categoryService = categoryService;
            _commentService = commentService;
        }

        async public Task<ServiceResult<WallViewModel>> getWall(int userID)
        {
            var Owner = await _userService.getById(userID);
            if (!Owner.IsOk()) return new ServiceResult<WallViewModel>(null, Owner.Code, Owner.Message);
            var Posts = await _postService.getAll(userID);
            if (!Posts.IsOk()) return new ServiceResult<WallViewModel>(null, Posts.Code, Posts.Message);
            var Categories = await _categoryService.getAll();
            if (!Categories.IsOk()) return new ServiceResult<WallViewModel>(null, Categories.Code, Categories.Message);


            var sortedPosts = Posts.Result
                          .OrderByDescending(x => x.IsPromoted).ThenByDescending(x => x.Datetime)
                          .ToList();


            SessionData.WallModel.Posts = sortedPosts;
            SessionData.WallModel.Owner = Owner.Result;
            SessionData.WallModel.Categories = Categories.Result;
            if (SessionData.FirstLoad)
            {
                SessionData.WallModel.SelectedCategories = Categories.Result.Select(x => x.CategoryName).ToList();
                SessionData.FirstLoad = false;
            }


            return new ServiceResult<WallViewModel>(SessionData.WallModel, System.Net.HttpStatusCode.OK, null);
        }



        public void ChangeCategoryFilterStatus(int categoryID)
        {
            string categoryName = _categoryService.getAll().Result.Result.Where(x => x.CategoryID == categoryID).FirstOrDefault().CategoryName;
            if (!SessionData.WallModel.SelectedCategories.Contains(categoryName)) SessionData.WallModel.SelectedCategories.Add(categoryName);
            else SessionData.WallModel.SelectedCategories.Remove(categoryName);
        }


        async public Task<ServiceResult<WallViewModel>> getUserPosts(int userID)
        {
            var Owner = await _userService.getById(userID);
            if (!Owner.IsOk()) return new ServiceResult<WallViewModel>(null, Owner.Code, Owner.Message);
            var Posts = await _postService.getUserPosts(userID);
            if (!Posts.IsOk()) return new ServiceResult<WallViewModel>(null, Posts.Code, Posts.Message);
            var Categories = await _categoryService.getAll();
            if (!Categories.IsOk()) return new ServiceResult<WallViewModel>(null, Categories.Code, Categories.Message);


            var sortedPosts = Posts.Result
                          .OrderByDescending(x => x.IsPromoted).ThenByDescending(x => x.Datetime)
                          .ToList();


            SessionData.WallModel.Posts = sortedPosts;
            SessionData.WallModel.Owner = Owner.Result;
            SessionData.WallModel.Categories = Categories.Result;
            if (SessionData.FirstLoad)
            {
                SessionData.WallModel.SelectedCategories = Categories.Result.Select(x => x.CategoryName).ToList();
                SessionData.FirstLoad = false;
            }


            return new ServiceResult<WallViewModel>(SessionData.WallModel, System.Net.HttpStatusCode.OK, null);
        }


        async public Task<ServiceResult<CommentListViewModel>> getUserComments(int userID)
        {
            var Owner = await _userService.getById(userID);
            if (!Owner.IsOk()) return new ServiceResult<CommentListViewModel>(null, Owner.Code, Owner.Message);
            var Comments = await _commentService.getByUserId(userID);
            if (!Comments.IsOk()) return new ServiceResult<CommentListViewModel>(null, Comments.Code, Comments.Message);


            var CommentsVM = new CommentListViewModel()
            {
                Comments = Comments.Result.ToList(),
                CommentsOwner = Owner.Result
            };

            return new ServiceResult<CommentListViewModel>(CommentsVM, System.Net.HttpStatusCode.OK, null);
        }


        async public Task<ServiceResult<PostEditViewModel>> getPostToEdit(int userID, int postID)
        {
            var Post = await _postService.getById(postID, userID);
            if (!Post.IsOk()) return new ServiceResult<PostEditViewModel>(null, Post.Code, Post.Message);
            var CurrentUser = await _userService.getById(userID);
            if (!CurrentUser.IsOk()) return new ServiceResult<PostEditViewModel>(null, CurrentUser.Code, CurrentUser.Message);
            var Categories = await _categoryService.getAll();
            if (!Categories.IsOk()) return new ServiceResult<PostEditViewModel>(null, Categories.Code, Categories.Message);

            var postVM = Post.Result;
            postVM.CurrentUser = CurrentUser.Result;

            var postEditVM = new PostEditViewModel
            {
                Post = postVM,
                AviableCategories = Categories.Result
            };


            return new ServiceResult<PostEditViewModel>(postEditVM, System.Net.HttpStatusCode.OK, null);
        }
    }
}
