using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WallProject.Models;
using WallProject.Models.DTO;
using WallProject.Models.Mapper;
using WallProject.Services.Services_Interfaces;

namespace WallProject.Services.Serives_Implementations
{
    public class CommentService : ICommentService
    {
        private readonly IHttpClientFactory clientFactory;
        public CommentService(IHttpClientFactory clientFactory)
        {
            this.clientFactory = clientFactory;
        }

        [HttpGet]
        async public Task<CommentViewModel> getById(int commentID)
        {
            var client = clientFactory.CreateClient("webapi");
            client.DefaultRequestHeaders.Add("userID", "1");
            var result = await client.GetAsync($"comment/{commentID}");

            if (result != null)
            {
                var jsonString = await result.Content.ReadAsStringAsync();

                var comment = JsonConvert.DeserializeObject<CommentDTO>(jsonString);
                if (comment != null)
                    return CommentMapper.Map(comment);
            }
            return null;
        }

        [HttpGet]
        async public Task<List<CommentViewModel>> getAll()
        {
            var client = clientFactory.CreateClient("webapi");
            client.DefaultRequestHeaders.Add("userID", "1");
            var result = await client.GetAsync("comments");

            if (result != null)
            {
                var jsonString = await result.Content.ReadAsStringAsync();

                var comment = JsonConvert.DeserializeObject<List<CommentDTO>>(jsonString);
                if (comment != null)
                    return CommentMapper.Map(comment);
            }
            return null;
        }

        async public Task<List<CommentViewModel>> getByPostId(int postID, int userID)
        {
            var client = clientFactory.CreateClient("webapi");
            client.DefaultRequestHeaders.Add("userID", $"{userID}");
            var result = await client.GetAsync($"post/{postID}/comments");
            if (result != null)
            {
                var jsonString = await result.Content.ReadAsStringAsync();

                var comments = JsonConvert.DeserializeObject<List<CommentDTO>>(jsonString);
                if (comments != null)
                    return CommentMapper.Map(comments);
            }
            return null;
        }
    }
}
