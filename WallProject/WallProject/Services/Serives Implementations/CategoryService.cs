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
    public class CategoryService: ICategoryService
    {
        private readonly IHttpClientFactory _clientFactory;
        public CategoryService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        async public Task<ServiceResult<List<CategoryViewModel>>> getAll()
        {
            var client = _clientFactory.CreateClient("webapi");
            var result = await client.GetAsync("categories");
            var jsonString = await result.Content.ReadAsStringAsync();


            if (result.IsSuccessStatusCode)
            {
                var categoryDTOs = JsonConvert.DeserializeObject<List<CategoryDTO>>(jsonString);
                return new ServiceResult<List<CategoryViewModel>>(Mapper.Map(categoryDTOs), result.StatusCode);
            }
            else
            {
                return ServiceResult<List<CategoryViewModel>>.GetMessage(jsonString, result.StatusCode);
            }

        
        }
    }
}
