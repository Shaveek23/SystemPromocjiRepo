using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WallProject.Models.MainView;

namespace WallProject.Services.Services_Interfaces
{
    public interface IUserService
    {
        public Task<ServiceResult<UserViewModel>> getById(int userID);
        public Task<ServiceResult<List<UserViewModel>>> getAll();
    }
}
