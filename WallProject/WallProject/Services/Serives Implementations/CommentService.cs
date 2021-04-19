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
using WallProject.Models.Mapper;
using WallProject.Services.Services_Interfaces;

namespace WallProject.Services.Serives_Implementations
{
    public class CommentService : ICommentService
    {
        //Dobra praktyka - pomaga uniknac problemu z wyczerpaniem gniazda
        private readonly IHttpClientFactory _clientFactory;
       
        public CommentService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
         
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
                ServiceResult<int?> likes = await getCommentLikes(commentDTO.commentID);
                return new ServiceResult<CommentViewModel>(Mapper.Map(commentDTO, likes.Result));
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
                List<CommentViewModel> commentsVM = new List<CommentViewModel>();
                List<CommentDTO> commentsDTO = JsonConvert.DeserializeObject<List<CommentDTO>>(jsonString);
                foreach (var commentDTO in commentsDTO)
                {
                    ServiceResult<int?> likes = await getCommentLikes(commentDTO.commentID);
                    commentsVM.Add(Mapper.Map(commentDTO, likes.Result));
                }
                return new ServiceResult<List<CommentViewModel>>(commentsVM);
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
                List<CommentViewModel> commentsVM = new List<CommentViewModel>();
                List<CommentDTO> commentsDTO = JsonConvert.DeserializeObject<List<CommentDTO>>(jsonString);
                foreach (var commentDTO in commentsDTO)
                {
                    ServiceResult<int?> likes = await getCommentLikes(commentDTO.commentID);
                    commentsVM.Add(Mapper.Map(commentDTO, likes.Result));
                }
                return new ServiceResult<List<CommentViewModel>>(commentsVM);
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

        async public Task AddNewComment(string commentText, int postId, int userId)
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
            if(!response.IsSuccessStatusCode)
            {
                throw new Exception("something went wrong");
            }


        }
    }
}
