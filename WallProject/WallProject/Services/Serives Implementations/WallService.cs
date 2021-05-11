﻿using Microsoft.AspNetCore.Mvc;
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
        private readonly IUserService _userService;
        private readonly ICategoryService _categoryService;
        public WallService(IPostService postService, IUserService userService, ICategoryService categoryService)
        {
            _postService = postService;
            _userService = userService;
            _categoryService = categoryService;
        }

        async public Task<ServiceResult<WallViewModel>> getWall(int userID)
        {
            var Owner = await _userService.getById(userID);
            if (!Owner.IsOk()) return new ServiceResult<WallViewModel>(null, Owner.Code, Owner.Message);
            var Posts = await _postService.getAll(userID);
            if (!Posts.IsOk()) return new ServiceResult<WallViewModel>(null, Posts.Code, Posts.Message);
            var Categories = await _categoryService.getAll();
            if (!Categories.IsOk()) return new ServiceResult<WallViewModel>(null, Posts.Code, Posts.Message);


            var sortedPosts = Posts.Result
                          .OrderByDescending(x => x.IsPromoted).ThenByDescending(x => x.Datetime)
                          .ToList();


            SessionData.WallModel.Posts = sortedPosts;
            SessionData.WallModel.Owner = Owner.Result;
            SessionData.WallModel.Categories = Categories.Result;
            return new ServiceResult<WallViewModel>(SessionData.WallModel, System.Net.HttpStatusCode.OK, null);
        }
        public void ChangeCategoryFilterStatus(int categoryId)
        {
            SessionData.WallModel.SelectedCategories[categoryId] = !SessionData.WallModel.SelectedCategories[categoryId];
        }
    }
}
