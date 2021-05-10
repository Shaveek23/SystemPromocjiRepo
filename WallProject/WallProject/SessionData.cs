using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
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
