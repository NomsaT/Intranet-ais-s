using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public static class BinOverview
    {
        public static IQueryable<DAL.DTO.Bins> getBinOverview()
        {
            return DAL.BinOverview.getBinOverview();
        }
    }
}
