using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WallProject.Models.MainView;

namespace WallProject.Services.Services_Interfaces
{
    public interface ICategoryService
    {
        public Task<ServiceResult<List<CategoryViewModel>>> getAll();
    }
}
