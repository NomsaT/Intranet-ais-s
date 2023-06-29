using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL
{
    public static class QuotationPDF
    {
        public static DAL.DTO.Quotation getQuotation(int QuotationId)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var vat = db.Vats.FirstOrDefault();
            var source = db.Quotes
                .Where(p => p.Id == QuotationId)
               .Select(p => new DAL.DTO.Quotation
               {
                   Id = p.Id,
                   DateCreated = (DateTime)p.SubmissionDate,
                   QuoteNo = p.Id,
                   RequestByFullName = p.RequestBy.Name + " " + p.RequestBy.Surname,
                   PlacedByFullName = p.PlacedBy.Name + " " + p.PlacedBy.Surname,
                   CustomerName = p.Customer.CompanyName,
                   CustomerAddressLine1 = p.Customer.PhysicalAddress.StreetAddress1,  
                   CustomerAddressLine2 = p.Customer.PhysicalAddress.StreetAddress2,
                   CustomerSuburb = p.Customer.PhysicalAddress.Suburb,
                   CustomerCity = p.Customer.PhysicalAddress.City,
                   CustomerPostalCode = p.Customer.PhysicalAddress.PostCode,
                   CustomerEmail = p.Customer.Contacts.Select(c=> c.ContactEmail).FirstOrDefault(),
                   CustomerTelephone = p.Customer.Contacts.Select(c=> c.ContactNumber).FirstOrDefault(),
                   CustomerPerson = p.Customer.Contacts.Select(c=> c.PersonName).FirstOrDefault(),
                   vatPerc = (int)vat.Vat1,
                   VAT = (vat.Vat1/100) * (decimal)p.QuoteItems.Sum(q => q.Price * q.Quantity) + (decimal)p.QuoteTransports.Sum(t => t.Price * t.Quantity),
                   Total = (decimal)p.QuoteItems.Sum(q=> q.Price * q.Quantity) + (decimal)p.QuoteTransports.Sum(t => t.Price * t.Quantity),
                   TotalAndVAT = (vat.Vat1 / 100) * (decimal)p.QuoteItems.Sum(q => q.Price * q.Quantity) + (decimal)p.QuoteTransports.Sum(t => t.Price * t.Quantity) + (decimal)p.QuoteItems.Sum(q => q.Price * q.Quantity) + (decimal)p.QuoteTransports.Sum(t => t.Price * t.Quantity),
                   QuoteItems = p.QuoteItems.Select(c => new DAL.DTO.QuoteItem
                   {
                       Id = c.Id,
                       Length = c.Length ,
                       Quantity = c.Quantity,
                       Price = c.Price ,
                       ProductId = c.ProductId,
                       ProductName = c.Product.ProductCode + " - " + c.Product.ProductName,
                       QuoteId = c.QuoteId,
                       Width = c.Width ,
                       m2Coil = (double)(c.Width * c.Length ),
                       priceCoil= (double)(c.Price / (c.Width * c.Length )),
                       Total = (decimal)(c.Price * c.Quantity)                     
                   }).ToList(),
                   QuoteTransports = p.QuoteTransports.Select(s => new DAL.DTO.QuoteTransport
                   {
                       Id = s.Id,
                       Description = s.Description,
                       Price = s.Price,
                       Quantity = s.Quantity,
                       Total = (decimal)(s.Price != null ? s.Price : 0 * s.Quantity != null ? s.Quantity : 0)
                   }).ToList(),
                   QuoteRevisions = p.QuoteRevisions.Select(r => new DAL.DTO.QuoteRevision
                   {
                       Id = r.Id,
                       QuoteId=r.QuoteId,
                       RevisionNr = r.RevisionNr
                   }).ToList(),
               }).FirstOrDefault();
            return source;
        }
    }
}
