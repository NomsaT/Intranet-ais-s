using DAL.Exceptions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DAL
{
    public static class Customer
    {
        public static IQueryable<DAL.DTO.AllCustomerDetails> getCustomers()
        {
            DAL.Models.AISContext dbContext = new DAL.Models.AISContext();
            var source = dbContext.Customers
               .Select(p => new DAL.DTO.AllCustomerDetails
               {
                   Id = p.Id,
                   CompanyName = p.CompanyName,
                   CompanyPrefix = p.CompanyPrefix,
                   PaymentMethodId = p.PaymentMethodId,
                   AccountNumber = p.AccountNumber,
                   Description = p.Description,
                   DifferentDelivery = p.DifferentDelivery,

                   PhysicalAddress = new DAL.DTO.CustomerAddress
                   {
                       StreetAddress1 = p.PhysicalAddress.StreetAddress1,
                       StreetAddress2 = p.PhysicalAddress.StreetAddress2,
                       Suburb = p.PhysicalAddress.Suburb,
                       City = p.PhysicalAddress.City,
                       PostCode = p.PhysicalAddress.PostCode,
                       CountryId = p.PhysicalAddress.CountryId
                   },
                   DeliveryAddress = new DAL.DTO.CustomerAddress
                   {
                       StreetAddress1 = p.DeliveryAddress.StreetAddress1,
                       StreetAddress2 = p.DeliveryAddress.StreetAddress2,
                       Suburb = p.DeliveryAddress.Suburb,
                       City = p.DeliveryAddress.City,
                       PostCode = p.DeliveryAddress.PostCode,
                       CountryId = p.DeliveryAddress.CountryId
                   },
                   Contacts = p.Contacts.Select(c => new DAL.DTO.CustomerContact
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

        public static async Task<int> addCustomer(string values)
        {
            #region Mapping


            DAL.Models.AISContext db = new DAL.Models.AISContext();

            var customer = new DAL.Models.Customer();
            JsonConvert.PopulateObject(values, customer);

            var check = db.Customers.Where(s => s.CompanyName == customer.CompanyName).FirstOrDefault();
            if (check != null)
            {
                throw new CustomerException("Customer already exists.");
            }

            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                try
                {

                    db.Customers.Add(customer);
                    if (customer.DeliveryAddress == null)
                    {
                        customer.DeliveryAddress = new Models.Address();
                    }

                    if (!customer.DifferentDelivery)
                    {
                        customer.DeliveryAddress.City = "";
                        customer.DeliveryAddress.StreetAddress1 = "";
                        customer.DeliveryAddress.StreetAddress2 = "";
                        customer.DeliveryAddress.Suburb = "";
                        customer.DeliveryAddress.PostCode = "";
                        customer.DeliveryAddress.CountryId = 190;

                    }
                    db.SaveChanges();
                    await dbContextTransaction.CommitAsync();
                    return customer.Id;

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    dbContextTransaction.Rollback();

                    throw new CustomerException("Error occuried while adding customer.");
                }
            }


            #endregion
        }

        public static async Task<int> updateCustomer(int key, string values)
        {

            DAL.Models.AISContext db = new DAL.Models.AISContext();

            var customer = await db.Customers.FirstOrDefaultAsync(i => i.Id == key);
            if (customer == null) throw new CustomerException("409, not found");

            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    db.Contacts.RemoveRange(customer.Contacts);
                    customer.Contacts.Clear();


                    JsonConvert.PopulateObject(values, customer);
                    var check = await db.Customers.Where(m => m.CompanyName.ToLower() == customer.CompanyName.ToLower() && m.Id != customer.Id).FirstOrDefaultAsync();
                    if (check != null)
                    {
                        dbContextTransaction.Rollback();
                        throw new CustomerException("Customer already exists.");
                    }

                    if (!customer.DifferentDelivery)
                    {
                        customer.DeliveryAddress.City = "";
                        customer.DeliveryAddress.StreetAddress1 = "";
                        customer.DeliveryAddress.StreetAddress2 = "";
                        customer.DeliveryAddress.Suburb = "";
                        customer.DeliveryAddress.PostCode = "";
                        customer.DeliveryAddress.CountryId = 190;

                    }

                    db.SaveChanges();
                    dbContextTransaction.Commit();
                    return customer.Id;

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    dbContextTransaction.Rollback();
                    throw new CustomerException("Error occured while updating customer.");
                }
            }

        }

        public static async Task<string> deleteCustomer(int key)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var customer = await db.Customers.FirstOrDefaultAsync(o => o.Id == key);
            if (customer == null) throw new CustomerException("409, not found");

            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    db.Contacts.RemoveRange(customer.Contacts);
                    db.Customers.Remove(customer);
                    db.Addresses.RemoveRange(customer.PhysicalAddress);
                    db.Addresses.RemoveRange(customer.DeliveryAddress);

                    await db.SaveChangesAsync();

                    dbContextTransaction.Commit();

                    return "Customer Deleted";

                }
                catch (Exception)
                {
                    dbContextTransaction.Rollback();
                    throw new CustomerException("Error occuried while deleting customer.");
                }


            }
        }

    }
}
