using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WallProject.Services.Services_Interfaces
{
    public interface INewsletterService
    {
        public Task<ServiceResult<bool>> subscribeCategory(int userID, int categoryID, bool isSubscribed);


    }
}
