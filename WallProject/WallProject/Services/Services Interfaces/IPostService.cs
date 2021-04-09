using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WallProject.Models;

namespace WallProject.Services.Services_Interfaces
{
    public interface IPostService
    {
        Task<List<PostViewModel>> GetByUserIdAsync(int userId);
        Task<PostViewModel> GetByPostIdAsync(int postId);
        Task<List<PostViewModel>> GetAllAsync();
    }
}