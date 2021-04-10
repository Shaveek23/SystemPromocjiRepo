using Microsoft.AspNetCore.Mvc;
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
        //Dobra praktyka - pomaga uniknac problemu z wyczerpaniem gniazda
        private readonly IHttpClientFactory clientFactory;
        private readonly IPostService _postService;
        private readonly IPersonService _personService;
        public WallService(IHttpClientFactory clientFactory, IPostService postService, IPersonService personService)
        {
            this.clientFactory = clientFactory;
            _postService = postService;
            _personService = personService;
        }
      
        [HttpGet]
        async public Task<WallViewModel> getWall(int userID)
        {
            var client = clientFactory.CreateClient("webapi");

            WallViewModel wallVM = new WallViewModel();
            wallVM.Owner = await _personService.getById(userID);
            wallVM.Posts = await _postService.getAll(userID);

            return wallVM;
        }
    }
}
