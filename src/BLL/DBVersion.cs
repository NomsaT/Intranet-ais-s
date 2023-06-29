using System.Linq;

namespace BLL
{
    public static class DBVersion
    {
        public static DAL.DTO.Dbversion getDBVersion()
        {
            return DAL.DBVersion.getDBVersion();
        }
    }
}
