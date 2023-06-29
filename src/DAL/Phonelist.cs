using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;

namespace DAL
{
    public static class Phonelist
    {
        public static IQueryable<DAL.DTO.Phonelist> getPhonelistSuppliers()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var source = db.Contacts
               .Select(p => new DAL.DTO.Phonelist
               {
                   Id = p.Id,
                   PersonName = p.PersonName,
                   ContactNumber = p.ContactNumber,
                   ContactEmail = p.ContactEmail,
                   WorkLandlineNumber = p.WorkLandlineNumber,
                   PersonPosition = p.PersonPosition,
                   SupplierId = p.SupplierId,
                   SupplierName = p.Supplier.CompanyName
               }).Where(s => s.SupplierId != null);
            return source;
        }

        public static IQueryable<DAL.DTO.Phonelist> getPhonelistCustomers()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var source = db.Contacts
               .Select(p => new DAL.DTO.Phonelist
               {
                   Id = p.Id,
                   PersonName = p.PersonName,
                   ContactNumber = p.ContactNumber,
                   ContactEmail= p.ContactEmail,
                   WorkLandlineNumber = p.WorkLandlineNumber,
                   PersonPosition = p.PersonPosition,
                   ContactId = p.ContactId,
                   CompanyName = p.ContactNavigation.CompanyName
               }).Where(s => s.ContactId != null);
            return source;
        }
    }
}
