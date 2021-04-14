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
                    ServiceResult<int?> likes =  await getCommentLikes(commentDTO.commentID);
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

        async public Task<ServiceResult<int?>> getCommentLikes(int commentID)
        {
            var client = _clientFactory.CreateClient("webapi");
            var result = await client.GetAsync($"comment/{commentID}/likedUsers");
            var jsonString = await result.Content.ReadAsStringAsync();

            //return new ServiceResult<int?>(jsonString, result.StatusCode);

            Random rand = new Random();
            return new ServiceResult<int?>(rand.Next(1, 10)); //na czas prezentacji, potem zmienić na 0
        }
    }
}
