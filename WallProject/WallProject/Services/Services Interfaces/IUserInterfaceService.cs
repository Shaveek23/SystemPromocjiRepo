using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WallProject.Models;
using WallProject.Models.MainView;

namespace WallProject.Services.Services_Interfaces
{
    public interface IUserInterfaceService
    {
        public Task<ServiceResult<UserInterfaceView>> getUserInterface(int userID);
        public Task<ServiceResult<bool>> SubscribeCategory(int userID, int categoryID, bool subscribe);

    }
}
