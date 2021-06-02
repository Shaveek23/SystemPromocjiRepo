using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models.POCO
{
    public interface IUserable
    {
        public int GetOwner();
    }
}
