using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WallProject.Models;
using WallProject.Services.Services_Interfaces;

namespace WallProject.Services.Serives_Implementations
{
    public class PersonService : IPersonService
    {
        //Dobra praktyka - pomaga uniknac problemu z wyczerpaniem gniazda
        private readonly IHttpClientFactory clientFactory;
        public PersonService(IHttpClientFactory clientFactory)
        {
            this.clientFactory = clientFactory;
        }

        [HttpGet]
        async public Task<PersonViewModel> getById(int userID)
        {
            var client = clientFactory.CreateClient("webapi");
            var result = await client.GetAsync($"person/{userID}");

            if (result != null)
            {
                var jsonString = await result.Content.ReadAsStringAsync();

                var user = JsonConvert.DeserializeObject<PersonViewModel>(jsonString);
                if (user != null)
                    return user;
            }
            return null;
        }

        [HttpGet]
        async public Task<List<PersonViewModel>> getAll()
        {
            var client = clientFactory.CreateClient("webapi");
            var result = await client.GetAsync("person");

            if (result != null)
            {
                var jsonString = await result.Content.ReadAsStringAsync();

                var users = JsonConvert.DeserializeObject<List<PersonViewModel>>(jsonString);
                if (users != null)
                    return users;
            }
            return null;
        }
    }
}
