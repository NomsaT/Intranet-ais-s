using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;

namespace DAL
{
    public static class VatStored
    {
        public static IQueryable<DAL.DTO.VatStored> getVatStored()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            var id = db.Suppliers.OrderByDescending(x => x.Id).Take(1).FirstOrDefault().Id;
            var source = db.VatStoreds
               .Select(p => new DAL.DTO.VatStored
               {
                   Id = p.Id,
                  GlcodeId = p.GlcodeId,
                  VatAmount = p.VatAmount
               }).OrderByDescending(x => x.Id).Take(1);
            return source;
        }

        public static IQueryable<DAL.DTO.Glcode> getGLCodes()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var source = db.Glcodes
               .Select(p => new DAL.DTO.Glcode
               {
                   Id = p.Id,
                   Code = p.Code,
                   Name = p.Name,
                   Description = p.Description,
                   Glfullname = p.Code + " - " + p.Name,
                   AssignVat = p.AssignVat
               }).Where(g => g.AssignVat == true);
            return source;
        }
    }
}
