using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WallProject.Models;

namespace WallProject.Services.Services_Interfaces
{
    public interface IWallService
    {
        public Task<ServiceResult<WallViewModel>> getWall(int userID);
    }
}
