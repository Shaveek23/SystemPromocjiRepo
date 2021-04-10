﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Database.Repositories.Interfaces;
using WebApi.Exceptions;
using WebApi.Models.DTO.PostDTOs;
using WebApi.Models.POCO;

namespace WebApi.Database.Repositories.Implementations
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        public PostRepository(DatabaseContext databaseContext) : base(databaseContext) { }

        #region TO DO :  zintegorować z generyczną metodą UpdateAsync

      

        public IQueryable<Comment> GetAllComments(int postID)
        {
            var comments = dbContext.Comments.Where(comment => comment.PostID == postID);
            //Wydaje mi sie ze nie trzeba zwracac wyjatky - brak komentarzy to nie bład
            if(comments==null)
            {
                return (IQueryable<Comment>)(new List<Comment>());
            }
            return comments;
        }

        #endregion
    }
}
