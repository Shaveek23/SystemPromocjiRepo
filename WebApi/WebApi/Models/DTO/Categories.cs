using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models.DTO
{
    public class Categories
    {
        public IQueryable<CategoryDTO> categories;
    }
}
