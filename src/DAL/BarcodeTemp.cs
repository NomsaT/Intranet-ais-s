using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;

namespace DAL
{
    public static class BarcodeTemp
    {
        public static DAL.DTO.BarcodeTemp getBarcodeTemp()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var source = db.BarcodeTemps
               .Select(p => new DAL.DTO.BarcodeTemp
               {
                   Id = p.Id,
                   Barcode = p.Barcode,
                   Template = p.Template
               }).First();
            return source;
        }

        public static DAL.DTO.BarcodeTemp getBarcodeProductTemp()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var source = db.BarcodeTemps
               .Select(p => new DAL.DTO.BarcodeTemp
               {
                   Id = p.Id,
                   Barcode = p.Barcode,
                   Template = p.Template
               }).OrderByDescending(i => i.Id).First();
            return source;
        }

        public static DAL.DTO.BarcodeTemp getBarcodeBinTemp()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var source = db.BarcodeTemps
               .Select(p => new DAL.DTO.BarcodeTemp
               {
                   Id = p.Id,
                   Barcode = p.Barcode,
                   Template = p.Template
               }).Where(b => b.Barcode == "Bin_QR").First();
            return source;
        }
    }
}
