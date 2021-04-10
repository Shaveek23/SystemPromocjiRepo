using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WallProject.Models;

namespace WallProject.Services.Services_Interfaces
{
    public interface IPersonService
    {
        public Task<PersonViewModel> getById(int userID);
        public Task<List<PersonViewModel>> getAll();
    }
}
