using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;

namespace DAL
{
    public static class Company
    {
        public static IQueryable<DAL.DTO.Company> getCompany()
        {

            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var source = db.Companies
                .Select(s => new DAL.DTO.Company
                {
                    Id = s.Id,
                    Name = s.Name,
                    VatNr = s.VatNr,
                    RegNr = s.RegNr,
                    MotherCompany = s.MotherCompany,
                    AddressId = s.AddressId,
                    Address = new DAL.DTO.SupplierAddress
                    {
                        StreetAddress1 = s.Address.StreetAddress1,
                        StreetAddress2 = s.Address.StreetAddress2,
                        Suburb = s.Address.Suburb,
                        City = s.Address.City,
                        CountryId = s.Address.CountryId,
                        PostCode = s.Address.PostCode,
                    },
                });
            return source;
        }

        public static int addCompany(string values)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            var Obj = new DAL.Models.Company();
            JsonConvert.PopulateObject(values, Obj);

            var check = db.Companies.Where(s => s.Name == Obj.Name).FirstOrDefault();
            if (check != null)
            {
                throw new CompanyException("Company already exists.");
            }

            if (Obj.MotherCompany == true)
            {
                var ismothercompany = db.Companies.Where(i => i.MotherCompany == true).FirstOrDefault();
                if (ismothercompany != null)
                {
                    throw new CompanyException(ismothercompany.Name + " Is already marked as the mother company.");
                }
            }

           

            db.Companies.Add(Obj);
            db.SaveChanges();
            return Obj.Id;
        }

        public static async Task<int> editCompany(int key, string values)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            var Obj = await db.Companies.FirstOrDefaultAsync(i => i.Id == key);
            if (Obj == null) throw new CompanyException("Company does not exist.");           

            JsonConvert.PopulateObject(values, Obj);
            var check = db.Companies.Where(m => m.Name == Obj.Name && m.Id != key).FirstOrDefault();
            if (check != null)
            {
                throw new CompanyException("Company already exists.");
            }

            if (Obj.MotherCompany == true)
            {
                var ismothercompany = db.Companies.Where(i => i.MotherCompany == true).FirstOrDefault();
                if (ismothercompany != null)
                {
                    throw new CompanyException(ismothercompany.Name + " Is already marked as the mother company.");
                }
            }

            db.SaveChanges();
            return Obj.Id;
        }

        public static async Task<int> deleteCompany(int key)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            var CompanyObj = await db.Companies.FirstOrDefaultAsync(s => s.Id == key);
            if (CompanyObj == null) throw new CompanyException("Company does not exist.");            

            if (CompanyObj.InternalOrders.Count > 0)
            {
                throw new CompanyException("Company is linked to an internal order.");
            }
          
            db.Companies.Remove(CompanyObj);
            db.Addresses.RemoveRange(CompanyObj.Address);

            db.SaveChanges();

            return key;
        }
    }
}
