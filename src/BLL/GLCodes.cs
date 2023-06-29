using System.Linq;
using System.Threading.Tasks;

namespace BLL
{
    public static class GLCodes
    {
        public static IQueryable<DAL.DTO.Glcode> getGLCodes()
        {
            return DAL.GLCodes.getGLCodes();
        }

        public static int addGLCodes(string values)
        {
            return DAL.GLCodes.addGLCodes(values);
        }
        public static Task<int> editGLCodes(int key, string values)
        {
            return DAL.GLCodes.editGLCodes(key, values);
        }

        public static Task<string> deleteGLCodes(int key)
        {
            return DAL.GLCodes.deleteGLCodes(key);
        }
    }
}
