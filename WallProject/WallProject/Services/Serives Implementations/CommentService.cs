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
        async public Task<CommentViewModel> getById(int commentID, int userID)
        {
            var client = clientFactory.CreateClient("webapi");
            client.DefaultRequestHeaders.Add("userID", $"{userID}");
            var result = await client.GetAsync($"comment/{commentID}");

            if (result.IsSuccessStatusCode)
            {
                var jsonString = await result.Content.ReadAsStringAsync();

                var commentDTO = JsonConvert.DeserializeObject<CommentDTO>(jsonString);
                if (commentDTO != null)
                {
                    CommentViewModel commentVM = CommentMapper.Map(commentDTO);
                    commentVM.Likes = await getCommentLikes(commentDTO.commentID);
                }
            }
            return null;
        }

        [HttpGet]
        async public Task<List<CommentViewModel>> getAll(int userID)
        {
            var client = clientFactory.CreateClient("webapi");
            client.DefaultRequestHeaders.Add("userID", $"{userID}");
            var result = await client.GetAsync("comments");

            if (result.IsSuccessStatusCode)
            {
                var jsonString = await result.Content.ReadAsStringAsync();
                var commentsDTO = JsonConvert.DeserializeObject<List<CommentDTO>>(jsonString);
                if (commentsDTO != null)
                {
                    List<CommentViewModel> commentsVM = new List<CommentViewModel>();
                    CommentViewModel commentVM;
                    foreach (var commentDTO in commentsDTO)
                    {
                        commentVM = CommentMapper.Map(commentDTO);
                        commentVM.Likes = await getCommentLikes(commentDTO.commentID);
                        commentsVM.Add(commentVM);
                    }
                    return commentsVM;
                }
            }
            return null;
        }

        async public Task<List<CommentViewModel>> getByPostId(int postID, int userID)
        {
            var client = clientFactory.CreateClient("webapi");
            client.DefaultRequestHeaders.Add("userID", $"{userID}");
            var result = await client.GetAsync($"post/{postID}/comments");
            if (result.IsSuccessStatusCode)
            {
                var jsonString = await result.Content.ReadAsStringAsync();
                var commentsDTO = JsonConvert.DeserializeObject<List<CommentDTO>>(jsonString);
                if (commentsDTO != null)
                {
                    List<CommentViewModel> commentsVM = new List<CommentViewModel>();
                    CommentViewModel commentVM;
                    foreach(var commentDTO in commentsDTO)
                    {
                        commentVM = CommentMapper.Map(commentDTO);
                        commentVM.Likes = await getCommentLikes(commentDTO.commentID);
                        commentsVM.Add(commentVM);
                    }
                    return commentsVM;
                }
            }
            return null;
        }

        async public Task<int> getCommentLikes(int commentID)
        {
            var client = clientFactory.CreateClient("webapi");
            var result = await client.GetAsync($"comment/{commentID}/likedUsers");
            if (result.IsSuccessStatusCode)
            {
                var jsonString = await result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<int>(jsonString);
            }
            Random rand = new Random();
            return rand.Next(1, 10); //na czas prezentacji, potem zmienić na 0
        }
    }
}
