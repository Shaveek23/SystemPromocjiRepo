using System.Collections.Generic;
using System.Linq;
using WallProject.Models;
using WallProject.Models.MainView;

namespace WallProject
{
    public static class SessionData
    {
        public static WallViewModel WallModel = new WallViewModel
        {
            Owner = null,
            Posts = new List<PostViewModel>(),
            Categories = new List<CategoryViewModel>(),
            SelectedCategories = Enumerable.Repeat(true, 100).ToArray()
        };
    }
}
