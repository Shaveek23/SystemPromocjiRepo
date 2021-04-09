using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WallProject.Models;
using WallProject.Services.Services_Interfaces;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace WallProject.Services.Serives_Implementations
{
    public class PostService: IPostService
    {
        private readonly IHttpClientFactory _clientFactory;
        public PostService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [HttpGet]
        public async Task<List<PostViewModel>> GetAllAsync()
        {
            var client = _clientFactory.CreateClient("webapi");
            var result = await client.GetAsync("post/{postId}");

            if (result != null)
            {
                var jsonString = await result.Content.ReadAsStringAsync();

                var posts = JsonConvert.DeserializeObject<List<PostViewModel>>(jsonString);
                if (posts != null)
                    return posts;
            }

            return null;
        }

        [HttpGet("{postId}")]
        public async Task<PostViewModel> GetByPostIdAsync(int postId)
        {
            var client = _clientFactory.CreateClient("webapi");
            var result = await client.GetAsync("post/{postId}");

            if (result != null)
            {
                var jsonString = await result.Content.ReadAsStringAsync();

                var post = JsonConvert.DeserializeObject<PostViewModel>(jsonString);
                if (post != null)
                    return post;
            }

            return null;
        }

        [HttpGet]
        [Route("post/{userId}")] //tu nie jestem pewny czy ten route dziala inaczej niz gdybym dal postId
        public async Task<List<PostViewModel>> GetByUserIdAsync(int userId)
        {
            var client = _clientFactory.CreateClient("webapi");
            var result = await client.GetAsync("post/{userId}");

            if (result != null)
            {
                var jsonString = await result.Content.ReadAsStringAsync();

                var posts = JsonConvert.DeserializeObject<List<PostViewModel>>(jsonString);
                if (posts != null)
                    return posts;
            }

            return null;
        }
    }

}