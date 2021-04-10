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
        public WallService(IHttpClientFactory clientFactory)
        {
            this.clientFactory = clientFactory;
        }
      
        [HttpGet]
        async public Task<PersonViewModel> getUser()
        {
            var client = clientFactory.CreateClient("webapi");
            var result = await client.GetAsync("person");
           
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
