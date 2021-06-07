using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using WallProject.Models.MainView;

namespace WallProject.Services.Services_Interfaces
{
    public interface IUserService
    {
        public Task<ServiceResult<UserViewModel>> getById(int userID);
        public Task<ServiceResult<List<UserViewModel>>> getAll();

        public Task<ServiceResult<bool>> EditUser(int userId, string userName, string userEmail);
    }
}
