using System.Linq;

namespace DAL
{
    public static class DBVersion
    {
        public static DAL.DTO.Dbversion getDBVersion()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            DAL.DTO.Dbversion source = new DAL.DTO.Dbversion {
                Version = db.Dbversions.First().Version
            };                

            return source;
        }
    }
}
