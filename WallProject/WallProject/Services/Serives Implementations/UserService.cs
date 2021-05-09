using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WallProject.Models;
using WallProject.Models.DTO;
using WallProject.Models.MainView;
using WallProject.Models.Mapper;
using WallProject.Services.Services_Interfaces;

namespace WallProject.Services.Serives_Implementations
{
    public class UserService : IUserService
    {
        private readonly IHttpClientFactory _clientFactory;
        public UserService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        async public Task<ServiceResult<UserViewModel>> getById(int userID)
        {
            var client = _clientFactory.CreateClient("webapi");
            var result = await client.GetAsync($"users/{userID}");
            var jsonString = await result.Content.ReadAsStringAsync();


            if (result.IsSuccessStatusCode)
            {
                var userDTO = JsonConvert.DeserializeObject<UserDTO>(jsonString);

                return new ServiceResult<UserViewModel>(Mapper.Map(userDTO), result.StatusCode);
            }
            else
            {
                return ServiceResult<UserViewModel>.GetMessage(jsonString, result.StatusCode);
            }
        }

        async public Task<ServiceResult<List<UserViewModel>>> getAll()
        {
            var client = _clientFactory.CreateClient("webapi");
            var result = await client.GetAsync($"users");
            var jsonString = await result.Content.ReadAsStringAsync();


            if (result.IsSuccessStatusCode)
            {
                var userDTOs = JsonConvert.DeserializeObject<List<UserDTO>>(jsonString);
                return new ServiceResult<List<UserViewModel>>(Mapper.Map(userDTOs), result.StatusCode);
            }
            else
            {
                return ServiceResult<List<UserViewModel>>.GetMessage(jsonString, result.StatusCode);
            }
        }


    }
}
