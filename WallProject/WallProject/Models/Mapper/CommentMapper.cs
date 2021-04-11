using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WallProject.Models.DTO;

namespace WallProject.Models.Mapper
{
    public class CommentMapper
    {
        public static CommentViewModel Map(CommentDTO commentDTO)
        {
            CommentViewModel commentVM = new CommentViewModel
            {
                Content = commentDTO.content,
                Time = commentDTO.dateTime,
                OwnerName = commentDTO.userID.ToString()
            };

            return commentVM;
        }

        public static List<CommentViewModel> Map(List<CommentDTO> commentsDTO)
        {
            List<CommentViewModel> list = new List<CommentViewModel>();
            foreach (var commentDTO in commentsDTO)
            {
                list.Add(Map(commentDTO));
            }

            return list;
        }
    }
}
