using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Database.Mapper;
using WebApi.Database.Repositories.Interfaces;
using WebApi.Models.DTO;
using WebApi.Services.Services_Interfaces;

namespace WebApi.Services.Serives_Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public ServiceResult<Categories> GetAll()
        {
            var result = _categoryRepository.GetAll();
            var categories = new Categories { categories = Mapper.Map(result.Result) };
            return new ServiceResult<Categories>(categories, result.Code, result.Message);
        }

        public ServiceResult<CategoryDTO> GetById(int categoryId)
        {
            var result = _categoryRepository.GetById(categoryId);
            return new ServiceResult<CategoryDTO>(Mapper.Map(result.Result), result.Code, result.Message);
        }


    }
}
