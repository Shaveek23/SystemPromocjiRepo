using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WallProject.Models;

namespace WallProject.Services.Services_Interfaces
{
    public interface IPersonService
    {
        public Task<ServiceResult<PersonViewModel>> getById(int userID);
        public Task<ServiceResult<List<PersonViewModel>>> getAll();
    }
}
