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
    public class NewsletterService : INewsletterService
    {
        private readonly IHttpClientFactory _clientFactory;
        public NewsletterService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }



        public async Task<ServiceResult<bool>> subscribeCategory(int userID, int categoryID, bool isSubscribed)
        {


                NewsletterDTO comment = new NewsletterDTO { CategoryID = categoryID, IsSubscribed = isSubscribed }; 

                //serializacja do JSONa
                var jsonComment = JsonConvert.SerializeObject(comment);
                //przygotowanie HttpRequest
                HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, $"newsletter");
                HttpContent httpContent = new StringContent(jsonComment, Encoding.UTF8, "application/json");
                requestMessage.Headers.Add("userID", userID.ToString());
                requestMessage.Content = httpContent;
                //Wysyłanie Request
                var client = _clientFactory.CreateClient("webapi");
                var response = await client.SendAsync(requestMessage);
                return new ServiceResult<bool>(response.IsSuccessStatusCode);

        }
    }
}
