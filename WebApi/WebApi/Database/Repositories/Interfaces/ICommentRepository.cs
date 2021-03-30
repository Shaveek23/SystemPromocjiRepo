﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models.POCO;

namespace WebApi.Database.Repositories.Interfaces
{
    public interface ICommentRepository : IRepository<Comment>
    {
       
    
        //TODO:Zamienic na Users
        Task<IQueryable<int> >GetLikedUsersAsync(int id);
       void  DeleteComment(int id);
       Task EditLikeOnComment(int commentId);
   
    }
}
