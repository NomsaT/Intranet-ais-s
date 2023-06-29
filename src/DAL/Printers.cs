using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;

namespace DAL
{
    public static class Printers
    {
        public static DAL.DTO.Printer getPrinter()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var source = db.Printers
               .Select(p => new DAL.DTO.Printer
               {
                   Id = p.Id,
                   Location = p.Location,
                   Name = p.Name
               }).First();
            return source;
        }

    }
}
