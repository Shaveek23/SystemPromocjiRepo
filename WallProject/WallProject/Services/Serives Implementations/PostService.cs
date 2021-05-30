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
            var result = await client.GetAsync("posts");
            var jsonString = await result.Content.ReadAsStringAsync();

            if (result.IsSuccessStatusCode)
            {
                var postsDTO = JsonConvert.DeserializeObject<List<PostGetDTO>>(jsonString);
                var usersResult = await _userService.getAll();
                if(!usersResult.IsOk())
                {
                    //Some information about it.  :TODO
                }
                var allComments = await _commentService.getAll(userID);

                List<PostViewModel> postVMs = new List<PostViewModel>();
                foreach (PostGetDTO postDTO in postsDTO)
                {
                    var postVM = Mapper.Map(postDTO);
                    postVM.Comments = allComments.Result.Where(x => x.PostID == postDTO.ID).ToList();
                    postVM.Owner = usersResult.Result?.Where(x => x.UserID == postDTO.AuthorID).FirstOrDefault();
                    postVM.CurrentUser = usersResult.Result?.Where(x => x.UserID == userID).FirstOrDefault();
                    postVMs.Add(postVM);
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
                var postDTO = JsonConvert.DeserializeObject<PostGetDTO>(jsonString);
                var postVM = Mapper.Map(postDTO);

                var usersResult = await _userService.getAll();
                if (usersResult.Result != null)
                {
                    postVM.CurrentUser = usersResult.Result?.Where(x => x.UserID == userID).FirstOrDefault();
                    postVM.Owner = usersResult.Result?.Where(x => x.UserID == postDTO.AuthorID).FirstOrDefault();
                }

                var comments = _commentService.getByPostId(postID, userID);
                if(comments.Result != null)
                {
                     postVM.Comments = comments.Result.Result;
                }
                return new ServiceResult<PostViewModel>(postVM);
            }
            else
            {
                return ServiceResult<PostViewModel>.GetMessage(jsonString, result.StatusCode);
            }
        }


        async public Task<ServiceResult<bool>> AddNewPost(string postText, int userId,int categoryId,string title)
        {
            //tworzenie komentarza na podstawie danych przekazanych z kontrolera
            PostCreateDTO post = new PostCreateDTO();

            post.Content = postText;
            //DO ZMIANY !!!

            post.CategoryID = categoryId;
         // zeby sie nie wywaloalo dodawanie 1 kategorii
            if (post.CategoryID == 0)
                post.CategoryID = 1;
            post.Title = title;
            if (title is null)
                post.Title = " No Title";


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
            PostChangeLikeStatusDTO postDTO = new PostChangeLikeStatusDTO { Like = like };

            //serializacja do JSONa
            var jsonComment = JsonConvert.SerializeObject(postDTO);
            //przygotowanie HttpRequest
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Put, $"post/{postID}/likedUsers");
            HttpContent httpContent = new StringContent(jsonComment, Encoding.UTF8, "application/json");
            requestMessage.Headers.Add("userId", userID.ToString());
            requestMessage.Content = httpContent;

            //Wysyłanie Request
            var client = _clientFactory.CreateClient("webapi");
            client.DefaultRequestHeaders.Add("userID", $"{userID}");
            var response = await client.SendAsync(requestMessage);
            return new ServiceResult<bool>(response.IsSuccessStatusCode);
        }

        public async Task<ServiceResult<List<PostViewModel>>> getUserPosts(int userID)
        {
            var client = _clientFactory.CreateClient("webapi");
            client.DefaultRequestHeaders.Add("userID", $"{userID}");
            var result = await client.GetAsync($"posts/{userID}");
            var jsonString = await result.Content.ReadAsStringAsync();

            if (result.IsSuccessStatusCode)
            {
                var postsDTO = JsonConvert.DeserializeObject<List<PostGetDTO>>(jsonString);
                var usersResult = await _userService.getAll();
                if (!usersResult.IsOk())
                {
                    //Some information about it.  :TODO
                }
                var allComments = await _commentService.getAll(userID);

                List<PostViewModel> postVMs = new List<PostViewModel>();
                foreach (PostGetDTO postDTO in postsDTO)
                {
                    var postVM = Mapper.Map(postDTO);
                    postVM.Comments = allComments.Result.Where(x => x.PostID == postDTO.ID).ToList();
                    postVM.Owner = usersResult.Result?.Where(x => x.UserID == postDTO.AuthorID).FirstOrDefault();
                    postVM.CurrentUser = usersResult.Result?.Where(x => x.UserID == userID).FirstOrDefault();
                    postVMs.Add(postVM);
                }
                return new ServiceResult<List<PostViewModel>>(postVMs, result.StatusCode, null);
            }
            else
            {
                return ServiceResult<List<PostViewModel>>.GetMessage(jsonString, result.StatusCode);
            }
        }

        async public Task<ServiceResult<bool>> EditPost(int userID, int postID, string title, string content, int categoryID, bool isPromoted)
        {
            PostPutDTO postDTO = new PostPutDTO();

            postDTO.Content = content;
            postDTO.Title = title;
            postDTO.CategoryID = categoryID;
            // zeby sie nie wywaloalo dodawanie 1 kategorii
            if (postDTO.CategoryID == 0)
                postDTO.CategoryID = 1;
            postDTO.IsPromoted = isPromoted;
            postDTO.Title = title;

            if (title is null)
                postDTO.Title = " No Title";


            //serializacja do JSONa
            var jsonComment = JsonConvert.SerializeObject(postDTO);
            //przygotowanie HttpRequest
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Put, $"post/{postID}");
            HttpContent httpContent = new StringContent(jsonComment, Encoding.UTF8, "application/json");
            requestMessage.Headers.Add("userID", userID.ToString());
            requestMessage.Content = httpContent;

            //Wysyłanie Request
            var client = _clientFactory.CreateClient("webapi");
            client.DefaultRequestHeaders.Add("userID", $"{userID}");
            var response = await client.SendAsync(requestMessage);
            return new ServiceResult<bool>(response.IsSuccessStatusCode);
        }

        async public Task<ServiceResult<bool>> DeletePost(int userID, int postID)
        {

            //przygotowanie HttpRequest
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Delete, $"post/{postID}");
            requestMessage.Headers.Add("userID", userID.ToString());

            //Wysyłanie Request
            var client = _clientFactory.CreateClient("webapi");
            client.DefaultRequestHeaders.Add("userID", $"{userID}");
            var response = await client.SendAsync(requestMessage);
            return new ServiceResult<bool>(response.IsSuccessStatusCode);
        }
    }
}
