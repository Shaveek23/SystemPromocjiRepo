using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WallProject.Models.MainView;

namespace WallProject.Models
{
    public class WallViewModel
    {
        public List<string> SelectedCategories;
        public UserViewModel Owner;
        public List<PostViewModel> Posts;
        public List<CategoryViewModel> Categories;
    }
}
