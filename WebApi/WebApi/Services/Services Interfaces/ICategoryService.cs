using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models.DTO;


namespace WebApi.Services.Services_Interfaces
{
    public interface ICategoryService
    {
        public ServiceResult<IQueryable<CategoryDTO>> GetAll();
        public ServiceResult<CategoryDTO> GetById(int categoryId);
    }
}
