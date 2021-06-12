using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WallProject.Models;
using WallProject.Models.DTO;
using WallProject.Models.DTO.UserDTOs;
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
                var userDTO = JsonConvert.DeserializeObject<UserGetDTO>(jsonString);

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
                var userDTOs = JsonConvert.DeserializeObject<List<UserGetDTO>>(jsonString);
                return new ServiceResult<List<UserViewModel>>(Mapper.Map(userDTOs), result.StatusCode);
            }
            else
            {
                return ServiceResult<List<UserViewModel>>.GetMessage(jsonString, result.StatusCode);
            }
        }
        public async Task<ServiceResult<bool>> EditUser(int userID, string userName, string userEmail)
        {
            UserPutDTO userDTO = new UserPutDTO { UserName = userName, UserEmail = userEmail, IsAdmin=true, IsActive=true, IsEntrepreneur=true, IsVerified=true };


            //serializacja do JSONa
            var jsonComment = JsonConvert.SerializeObject(userDTO);
            //przygotowanie HttpRequest
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Put, $"users/{userID}");
            HttpContent httpContent = new StringContent(jsonComment, Encoding.UTF8, "application/json");
            requestMessage.Headers.Add("UserID", userID.ToString());
            requestMessage.Content = httpContent;

            //Wysyłanie Request
            var client = _clientFactory.CreateClient("webapi");
            //client.DefaultRequestHeaders.Add("UserID", $"{userID}");
            var response = await client.SendAsync(requestMessage);
            return new ServiceResult<bool>(response.IsSuccessStatusCode);
        }
        public async Task<ServiceResult<List<int>>> getAllSubscribedCategoriesID(int userID)
        {

            var client = _clientFactory.CreateClient("webapi");
            client.DefaultRequestHeaders.Add("UserID", $"{userID}");
            var result = await client.GetAsync($"users/{userID}/subscibedCategories");
            var jsonString = await result.Content.ReadAsStringAsync();


            if (result.IsSuccessStatusCode)
            {
                List<int> ids = JsonConvert.DeserializeObject<List<int>>(jsonString);

                return new ServiceResult<List<int>>(ids, result.StatusCode);
            }
            else
            {
                return ServiceResult<List<int>>.GetMessage(jsonString, result.StatusCode);
            }
        }

    }
}
