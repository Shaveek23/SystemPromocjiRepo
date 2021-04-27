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
using WallProject.Models.DTO.CommentDTOs;
using WallProject.Models.Mapper;
using WallProject.Services.Services_Interfaces;

namespace WallProject.Services.Serives_Implementations
{
    public class CommentService : ICommentService
    {
        //Dobra praktyka - pomaga uniknac problemu z wyczerpaniem gniazda
        private readonly IHttpClientFactory _clientFactory;

        private readonly IUserService _userService;

        public CommentService(IHttpClientFactory clientFactory, IUserService userService)
        {
            _clientFactory = clientFactory;
            _userService = userService;
         
        }

        async public Task<ServiceResult<CommentViewModel>> getById(int commentID, int userID)
        {
            var client = _clientFactory.CreateClient("webapi");
            client.DefaultRequestHeaders.Add("userID", $"{userID}");
            var result = await client.GetAsync($"comment/{commentID}");

            var jsonString = await result.Content.ReadAsStringAsync();

            if (result.IsSuccessStatusCode)
            {
                var commentDTO = JsonConvert.DeserializeObject<CommentDTO>(jsonString);
                var commentVM = Mapper.Map(commentDTO);
                commentVM.Owner = _userService.getById(commentDTO.authorID).Result.Result;

                return new ServiceResult<CommentViewModel>(commentVM);
            }
            else
            {
                return ServiceResult<CommentViewModel>.GetMessage(jsonString, result.StatusCode);
            }
        }

        async public Task<ServiceResult<List<CommentViewModel>>> getAll(int userID)
        {
            var client = _clientFactory.CreateClient("webapi");
            client.DefaultRequestHeaders.Add("userID", $"{userID}");
            var result = await client.GetAsync($"comments");
            var jsonString = await result.Content.ReadAsStringAsync();

            if (result.IsSuccessStatusCode)
            {
                List<CommentViewModel> commentVMs = new List<CommentViewModel>();
                List<CommentDTO> commentDTOs = JsonConvert.DeserializeObject<List<CommentDTO>>(jsonString);

                var users = await _userService.getAll();

                foreach (var commentDTO in commentDTOs)
                {
                    var commentVM = Mapper.Map(commentDTO);
                    commentVM.Owner = users.Result?.Where(x => x.UserID == commentDTO.authorID).FirstOrDefault();
                    commentVMs.Add(commentVM);
                }
                return new ServiceResult<List<CommentViewModel>>(commentVMs);
            }
            else
            {
                return ServiceResult<List<CommentViewModel>>.GetMessage(jsonString, result.StatusCode);
            }
        }

        async public Task<ServiceResult<List<CommentViewModel>>> getByPostId(int postID, int userID)
        {
            var client = _clientFactory.CreateClient("webapi");
            client.DefaultRequestHeaders.Add("userID", $"{userID}");
            var result = await client.GetAsync($"post/{postID}/comments");
            var jsonString = await result.Content.ReadAsStringAsync();

            if (result.IsSuccessStatusCode)
            {
                List<CommentViewModel> commentVMs = new List<CommentViewModel>();
                List<CommentDTO> commentDTOs = JsonConvert.DeserializeObject<List<CommentDTO>>(jsonString);

                var users = await _userService.getAll();

                foreach (var commentDTO in commentDTOs)
                {
                    var commentVM = Mapper.Map(commentDTO);
                    commentVM.Owner = users.Result?.Where(x => x.UserID == commentDTO.authorID).FirstOrDefault();
                    commentVMs.Add(commentVM);
                }
                return new ServiceResult<List<CommentViewModel>>(commentVMs);
            }
            else
            {
                return ServiceResult<List<CommentViewModel>>.GetMessage(jsonString, result.StatusCode);
            }
        }

        //TO DO: teoretycznie powinno to zwracać listę ID uzytkowinków którzy polajkwoali
        // ale narazie zwraca jakby Count tej listy, ale skoro nie zwracamy tego z WallApi
        // to zwracam  tutaj random 
        // można poprawić w Api to że zapytanie o Comment będzie zwracać odrazu ilość lików
        // jak to jest w przypadku Posta
        async public Task<ServiceResult<int?>> getCommentLikes(int commentID)
        {
            var client = _clientFactory.CreateClient("webapi");
            var result = await client.GetAsync($"comment/{commentID}/likedUsers");
            var jsonString = await result.Content.ReadAsStringAsync();

            //return new ServiceResult<int?>(jsonString, result.StatusCode);

            Random rand = new Random();
            return new ServiceResult<int?>(rand.Next(1, 10)); //na czas prezentacji, potem zmienić na 0
        }

        async public Task<ServiceResult<bool>> AddNewComment(string commentText, int postId, int userId)
        {
            //tworzenie komentarza na podstawie danych przekazanych z kontrolera
          
            CommentDTONoID comment = new CommentDTONoID();
            comment.content = commentText;
            comment.postID = postId;
            comment.userID = userId;
            comment.dateTime = DateTime.Now;
          
            //serializacja do JSONa
            var jsonComment = JsonConvert.SerializeObject(comment);
            //przygotowanie HttpRequest
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, $"comment");
            HttpContent httpContent = new StringContent(jsonComment, Encoding.UTF8, "application/json");
            requestMessage.Headers.Add("userId", userId.ToString());
            requestMessage.Content = httpContent;
            //Wysyłanie Request
            var client = _clientFactory.CreateClient("webapi");
            var response = await client.SendAsync(requestMessage);
            return new ServiceResult<bool>(response.IsSuccessStatusCode);
           ;

        }

        async public Task<ServiceResult<bool>> EditLikeStatus(int commentID, int userID, bool like)
        {
            //tworzenie komentarza na podstawie danych przekazanych z kontrolera          
            CommentChangeLikeStatusDTO postDTO = new CommentChangeLikeStatusDTO { like = like };

            //serializacja do JSONa
            var jsonComment = JsonConvert.SerializeObject(postDTO);
            //przygotowanie HttpRequest
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Put, $"comment/{commentID}/likeUsers");
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
