using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WallProject.Models
{
    public static class IdCoder
    {
        private static Dictionary<int, int> FrontToOrginal = new Dictionary<int, int>();
        private static Dictionary<int, int>  OrginalToFront = new Dictionary<int, int>();
        public static int CreateFrontId(int id)
        {
            //jak juz jest
            if (OrginalToFront.ContainsKey(id)) return OrginalToFront[id];
            FrontToOrginal.Add(FrontToOrginal.Count, id);
            OrginalToFront.Add(id, FrontToOrginal.Count - 1);
            return (FrontToOrginal.Count-1);
        }
        public static int GetOrginalId(int frontId) => FrontToOrginal[frontId];
        public static int GetFrontId(int orginalId) => OrginalToFront[orginalId];
       
        
    }
}
