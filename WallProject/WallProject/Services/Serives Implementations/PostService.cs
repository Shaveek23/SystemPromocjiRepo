using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WallProject.Models;
using WallProject.Models.DTO;
using WallProject.Models.DTO.PostDTOs;
using WallProject.Models.Mapper;
using WallProject.Services.Services_Interfaces;

namespace WallProject.Services.Serives_Implementations
{
    public class PostService : IPostService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ICommentService _commentService;
        private readonly IUserService _userService;
        public PostService(IHttpClientFactory clientFactory, ICommentService commentService, IUserService userService)
        {
            _clientFactory = clientFactory;
            _commentService = commentService;
            _userService = userService; 
        }


        public async Task<ServiceResult<List<PostViewModel>>> getAll(int userID)
        {
            var client = _clientFactory.CreateClient("webapi");
            client.DefaultRequestHeaders.Add("userID", $"{userID}");
            var result = await client.GetAsync("post");
            var jsonString = await result.Content.ReadAsStringAsync();

            if (result.IsSuccessStatusCode)
            {
                var postsDTO = JsonConvert.DeserializeObject<List<PostDTO>>(jsonString);
                var usersResult = await _userService.getAll();
                if(!usersResult.IsOk())
                {
                    //Some information about it.  :TODO
                }


                List<PostViewModel> postVMs = new List<PostViewModel>();
                foreach (PostDTO postDTO in postsDTO)
                {
                    var commentsResult = await _commentService.getByPostId(postDTO.id, userID);
                    if (!commentsResult.IsOk())
                    {
                        return new ServiceResult<List<PostViewModel>>(null, commentsResult.Code, commentsResult.Message);
                    }
                    else
                    {
                        var postVM = Mapper.Map(postDTO, commentsResult.Result);
                        postVM.Owner = usersResult.Result?.Where(x => x.UserID == postDTO.authorID).FirstOrDefault();
                        postVMs.Add(postVM);
                    }
                   
                }
                return new ServiceResult<List<PostViewModel>>(postVMs, result.StatusCode, null);
            }
            else
            {
                return ServiceResult<List<PostViewModel>>.GetMessage(jsonString, result.StatusCode);
            }
        }

        async public Task<ServiceResult<PostViewModel>> getById(int postID, int userID)
        {
            var client = _clientFactory.CreateClient("webapi");
            client.DefaultRequestHeaders.Add("userID", $"{userID}");
            var result = await client.GetAsync($"post/{postID}");
            var jsonString = await result.Content.ReadAsStringAsync();

            if (result.IsSuccessStatusCode)
            {
                var postDTO = JsonConvert.DeserializeObject<PostDTO>(jsonString);
                var commentsResult = await _commentService.getByPostId(postDTO.id, userID);
                if (!commentsResult.IsOk())
                    return new ServiceResult<PostViewModel>(null, commentsResult.Code, commentsResult.Message);
                else
                    return new ServiceResult<PostViewModel>(Mapper.Map(postDTO, commentsResult.Result));
            }
            else
            {
                return ServiceResult<PostViewModel>.GetMessage(jsonString, result.StatusCode);
            }
        }
        async public Task<ServiceResult<bool>> AddNewPost(string postText, int userId)
        {
            //tworzenie komentarza na podstawie danych przekazanych z kontrolera
            PostDTONoID post = new PostDTONoID();

            post.content = postText;
            post.datetime = DateTime.Now;
            //DO ZMIANY !!!

            post.category = 1;
            post.isPromoted = false;
            post.title = "brak";

            //serializacja do JSONa
            var jsonComment = JsonConvert.SerializeObject(post);
            //przygotowanie HttpRequest
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, $"post");
            HttpContent httpContent = new StringContent(jsonComment, Encoding.UTF8, "application/json");
            requestMessage.Headers.Add("userId", userId.ToString());
            requestMessage.Content = httpContent;

            //Wysyłanie Request
            var client = _clientFactory.CreateClient("webapi");
            client.DefaultRequestHeaders.Add("userID", $"{userId}");
            var response = await client.SendAsync(requestMessage);
            return new ServiceResult<bool>(response.IsSuccessStatusCode); 
        }


        async public Task<ServiceResult<bool>> EditLikeStatus(int postID, int userID, bool like)
        {
            //tworzenie komentarza na podstawie danych przekazanych z kontrolera          
            PostChangeLikeStatusDTO postDTO = new PostChangeLikeStatusDTO { like = like };

            //serializacja do JSONa
            var jsonComment = JsonConvert.SerializeObject(postDTO);
            //przygotowanie HttpRequest
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Put, $"post/{postID}/likeUsers");
            HttpContent httpContent = new StringContent(jsonComment, Encoding.UTF8, "application/json");
            requestMessage.Headers.Add("userId", userID.ToString());
            requestMessage.Content = httpContent;

            //Wysyłanie Request
            var client = _clientFactory.CreateClient("webapi");
            client.DefaultRequestHeaders.Add("userID", $"{userID}");
            var response = await client.SendAsync(requestMessage);
            return new ServiceResult<bool>(response.IsSuccessStatusCode);
        }

    }
}
