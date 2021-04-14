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
        private readonly IHttpClientFactory _clientFactory;
        public PersonService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        async public Task<ServiceResult<PersonViewModel>> getById(int userID)
        {
            var client = _clientFactory.CreateClient("webapi");
            var result = await client.GetAsync($"person/{userID}");
            var jsonString = await result.Content.ReadAsStringAsync();

            return new ServiceResult<PersonViewModel>(jsonString, result.StatusCode);
        }

        async public Task<ServiceResult<List<PersonViewModel>>> getAll()
        {
            var client = _clientFactory.CreateClient("webapi");
            var result = await client.GetAsync("person");
            var jsonString = await result.Content.ReadAsStringAsync();

            return new ServiceResult<List<PersonViewModel>>(jsonString, result.StatusCode);
        }
    }
}
