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
        
        private readonly IHttpClientFactory clientFactory;
        private string BaseUrl = "https://webapi20210317153051.azurewebsites.net/api/";
       

        public WallService(IHttpClientFactory clientFactory)
        {

            this.clientFactory = clientFactory;
           

        }
      
        [HttpGet]
        async public Task<PersonViewModel> getUser()
        {
            var client = clientFactory.CreateClient();
            var result = await client.GetAsync(BaseUrl+"person");
           
            if(result!=null)
            {
                var jsonString = await result.Content.ReadAsStringAsync();
                
               var user= JsonConvert.DeserializeObject<List<PersonViewModel>>(jsonString);
                if(user!=null)
                return user[0];
            }
          

            return null;
        }
    }
}
