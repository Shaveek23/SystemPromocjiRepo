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
    public class PostService : IPostService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ICommentService _commentService;
        private readonly IPersonService _personService;
        public PostService(IHttpClientFactory clientFactory, ICommentService commentService, IPersonService personService)
        {
            _clientFactory = clientFactory;
            _commentService = commentService;
            _personService = personService; //TO DO: tutaj będzie podstawiany userService, narazie korzystamy z Person
        }

        public async Task<List<PostViewModel>> getAll(int userID)
        {
            var client = _clientFactory.CreateClient("webapi");
            client.DefaultRequestHeaders.Add("userID", $"{userID}");
            var result = await client.GetAsync("post");

            if (result.IsSuccessStatusCode)
            {
                var jsonString = await result.Content.ReadAsStringAsync();

                var postsDTO = JsonConvert.DeserializeObject<List<PostDTO>>(jsonString);
                if (postsDTO != null)
                {
                    List<PostViewModel> postsVM = new List<PostViewModel>();
                    PostViewModel postVM;
                    foreach(PostDTO postDTO in postsDTO)
                    {
                        postVM = PostMapper.Map(postDTO);
                        postVM.Comments = await _commentService.getByPostId(postDTO.id, userID);
                        //postVM.Owner = await _personService.getById(userID);
                        postsVM.Add(postVM);
                    }
                    return postsVM;
                }
            }
            return null;
        }

        [HttpGet]
        async public Task<PostViewModel> getById(int postID, int userID)
        {
            var client = _clientFactory.CreateClient("webapi");
            client.DefaultRequestHeaders.Add("userID", $"{userID}");
            var result = await client.GetAsync($"post/{postID}");

            if (result.IsSuccessStatusCode)
            {
                var jsonString = await result.Content.ReadAsStringAsync();

                var post = JsonConvert.DeserializeObject<PostDTO>(jsonString);
                if (post != null)
                {
                    PostViewModel postVM = PostMapper.Map(post); 
                    postVM.Comments = await _commentService.getByPostId(postID, userID);
                    //postVM.Owner = await _personService.getById(userID);
                    return postVM;
                }
            }
            return null;
        }
    }
}
