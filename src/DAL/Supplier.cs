using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;

namespace DAL
{
    public static class Supplier
    {
        public static IQueryable<DAL.DTO.Supplier> getSupplier()
        {

            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var source = db.Suppliers
                .Select(s => new DAL.DTO.Supplier
                {
                    Id = s.Id,                    
                    CompanyName = s.CompanyName,
                    SupplierFullName = s.CompanyName,
                    CreditLimit = s.CreditLimit,
                    CreditTerms = s.CreditTerms,
                    Discount = s.Discount,
                    AddressId = s.AddressId,
                    Comment = s.Comment,
                    BankNameId = s.BankNameId,
                    AccountNumber = s.AccountNumber,
                    BranchCode = s.BranchCode,
                    PaymentMethodId = s.PaymentMethodId,
                    CurrencyId = s.CurrencyId,
                    Address = new DAL.DTO.SupplierAddress
                    {
                        StreetAddress1 = s.Address.StreetAddress1,
                        StreetAddress2 = s.Address.StreetAddress2,
                        Suburb = s.Address.Suburb,
                        City = s.Address.City,
                        CountryId = s.Address.CountryId,
                        PostCode = s.Address.PostCode,
                    },
                    Contacts = s.Contacts.Select(c => new DAL.DTO.SupplierContact
                    {
                        PersonName = c.PersonName,
                        PersonPosition = c.PersonPosition,
                        ContactNumber = c.ContactNumber,
                        ContactEmail = c.ContactEmail,
                        WorkLandlineNumber = c.WorkLandlineNumber
                    }).ToList()

                });
            return source;
        }

        public static int addSupplier(string values)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            var supplier = new DAL.Models.Supplier();
            JsonConvert.PopulateObject(values, supplier);

            var check = db.Suppliers.Where(s => s.CompanyName == supplier.CompanyName).FirstOrDefault();
            if (check != null)
            {
                throw new SupplierException("Supplier code already exists.");
            }

            db.Suppliers.Add(supplier);
            db.SaveChanges();
            return supplier.Id;
        }

        public static async Task<int> editSupplier(int key, string values)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            var supplier = await db.Suppliers.FirstOrDefaultAsync(i => i.Id == key);
            if (supplier == null) throw new SupplierException("Supplier does not exist.");

            db.Contacts.RemoveRange(supplier.Contacts);
            supplier.Contacts.Clear();

            JsonConvert.PopulateObject(values, supplier);
            var check = db.Suppliers.Where(m => m.CompanyName == supplier.CompanyName && m.Id != key).FirstOrDefault();
            if (check != null)
            {
                throw new SupplierException("Supplier already exists.");
            }

            db.SaveChanges();
            return supplier.Id;
        }

        public static async Task<int> deleteSupplier(int key)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            var supplierObj = await db.Suppliers.FirstOrDefaultAsync(s => s.Id == key);
            if (supplierObj == null) throw new SupplierException("Supplier does not exist.");

            if (supplierObj.PriceLookups.Count > 0)
            {
                throw new SupplierException("Price Lookup contains items of supplier");
            }

            if (supplierObj.Stocks.Count > 0)
            {
                throw new SupplierException("Stock contains items of supplier.");
            }

            if(supplierObj.InternalOrders.Count > 0)
            {
                throw new SupplierException("Supplier is linked to an Internal Order.");
            }

            db.Contacts.RemoveRange(supplierObj.Contacts);            
            db.Suppliers.Remove(supplierObj);
            db.Addresses.RemoveRange(supplierObj.Address);

            db.SaveChanges();

            return key;
        }
    }
}
