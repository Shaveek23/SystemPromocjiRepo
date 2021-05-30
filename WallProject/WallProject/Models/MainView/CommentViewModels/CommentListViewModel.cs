using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WallProject.Models.MainView;

namespace WallProject.Models
{
    public class CommentListViewModel
    {
        public List<CommentViewModel> Comments;
        public UserViewModel CommentsOwner;
    }
}
