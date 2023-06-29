using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL
{
    public static class Quotations
    {
        public static IQueryable<DAL.DTO.Quote> getQuotations()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            var source = db.Quotes
               .Select(p => new DAL.DTO.Quote
               {
                   Id = p.Id,
                   QuoteStatusId = p.QuoteStatusId,
                   QuoteStatusDisplay = p.QuoteStatus.Name,
                   RequestById = p.RequestById,
                   PlacedById = p.PlacedById,
                   SubmissionDate = p.SubmissionDate,
                   ValidFor = p.ValidFor,
                   DaysFrom = p.DaysFrom,
                   OnOrder = p.OnOrder,
                   OnDelivery = p.OnDelivery,
                   CustomerId = p.CustomerId,
                   PlacedByFullName = p.PlacedBy.Name + " - " + p.PlacedBy.Surname,
                   RequestByFullName = p.RequestBy.Name + " - " + p.RequestBy.Surname,
                   CompanyName = p.Customer.CompanyName,
                   QuoteItems = p.QuoteItems.Select(c => new DAL.DTO.QuoteItem
                   {
                      Id = c.Id,
                      ProductId = c.ProductId,
                      Width = c.Width,
                      Length = c.Length,
                      Price = c.Price,
                      Quantity = c.Quantity,
                      QuoteId = c.QuoteId
                   }).ToList(),
                   QuoteTransports = p.QuoteTransports.Select(t => new DAL.DTO.QuoteTransport
                   {
                       Id = t.Id,
                       Description = t.Description,
                       Price = t.Price,
                       Quantity=t.Quantity,
                       QuoteId=t.QuoteId
                   }).ToList(),
                   QuoteRevisions = p.QuoteRevisions.Select(r => new DAL.DTO.QuoteRevision
                   {
                       Id = r.Id,
                       QuoteId = r.QuoteId,
                       RevisionNr = r.RevisionNr
                   }).ToList(),
               }).OrderByDescending(i => i.Id);

            return source;
        }

        public static DAL.DTO.Quote getQuotationsDataEdit(int quoteId)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            DAL.DTO.Quote data = new DAL.DTO.Quote();

            var IO = db.Quotes.Find(quoteId);


            data = new DAL.DTO.Quote
            {
                Id = IO.Id,
                QuoteStatusId = IO.QuoteStatusId,
                QuoteStatusDisplay = IO.QuoteStatus.Name,
                RequestById = IO.RequestById,
                RequestByFullName = IO.RequestBy.Name + " " + IO.RequestBy.Surname,
                PlacedById = IO.PlacedById,
                PlacedByFullName = IO.PlacedBy.Name + " " + IO.PlacedBy.Surname,
                SubmissionDate = IO.SubmissionDate,
                ValidFor = IO.ValidFor,
                DaysFrom = IO.DaysFrom,
                OnOrder = IO.OnOrder,
                OnDelivery = IO.OnDelivery,
                CustomerId = IO.CustomerId,
                CompanyName = IO.Customer.CompanyName,
            };

            data.QuoteItems = IO.QuoteItems.Select(c => new DAL.DTO.QuoteItem
            {
                Id = c.Id,
                ProductId = c.ProductId,
                Width = c.Width,
                Length = c.Length,
                Price = c.Price,
                Quantity = c.Quantity,
                QuoteId = c.QuoteId
            }).ToList();

            data.QuoteTransports = IO.QuoteTransports.Select(t => new DAL.DTO.QuoteTransport
            {
                Id = t.Id,
                Description = t.Description,
                Price = t.Price,
                Quantity = t.Quantity,
                QuoteId = t.QuoteId
            }).ToList();

            data.QuoteRevisions = IO.QuoteRevisions.Select(r => new DAL.DTO.QuoteRevision
            {
                Id = r.Id,
                QuoteId = r.QuoteId,
                RevisionNr = r.RevisionNr
            }).ToList();
            return data;
        }

        public static int addQuotations(DAL.DTO.Quote values)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var Obj = new DAL.Models.Quote();
            JsonConvert.PopulateObject(JsonConvert.SerializeObject(values), Obj);

            Obj.SubmissionDate = DateTime.Now;
            Obj.QuoteStatusId = (int)DAL.Constants.QuotationsStatus.COMPLETED;
            db.Quotes.Add(Obj);
            db.SaveChanges();

            var ObjRevision = new DAL.Models.QuoteRevision();
            ObjRevision.QuoteId = Obj.Id;
            ObjRevision.RevisionNr = 1;
            db.QuoteRevisions.Add(ObjRevision);
            db.SaveChanges();

            return Obj.Id;
        }

        public static int addQuotationsDraft(DAL.DTO.Quote values)
        {
            //save a draft of the quotation
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var Obj = new DAL.Models.Quote();
            JsonConvert.PopulateObject(JsonConvert.SerializeObject(values), Obj);

            Obj.SubmissionDate = DateTime.Now;
            Obj.QuoteStatusId = (int)DAL.Constants.QuotationsStatus.DRAFT;
            db.Quotes.Add(Obj);
            db.SaveChanges();

            var ObjRevision = new DAL.Models.QuoteRevision();
            ObjRevision.QuoteId = Obj.Id;
            ObjRevision.RevisionNr = 1;
            db.QuoteRevisions.Add(ObjRevision);
            db.SaveChanges();

            return Obj.Id;
        }

        public static int editQuotations(int key, DAL.DTO.Quote values)
        {

            DAL.Models.AISContext db = new DAL.Models.AISContext();

            var Obj = db.Quotes.FirstOrDefault(o => o.Id == key);
            if (Obj == null) throw new QuotationException("Quotation does not exist.");

            DTO.Quote changes = new DTO.Quote();
            var serializerSettings = new JsonSerializerSettings { ObjectCreationHandling = ObjectCreationHandling.Replace };
            JsonConvert.PopulateObject(JsonConvert.SerializeObject(values), changes, serializerSettings);

            if (changes.QuoteItems != null)
            {
                db.QuoteItems.RemoveRange(Obj.QuoteItems);
                Obj.QuoteItems.Clear();
            }
            if (changes.QuoteTransports != null)
            {
                db.QuoteTransports.RemoveRange(Obj.QuoteTransports);
                Obj.QuoteTransports.Clear();
            }

            JsonConvert.PopulateObject(JsonConvert.SerializeObject(values), Obj, serializerSettings);

            db.SaveChanges();

            if(values.QuoteStatusId == (int)DAL.Constants.QuotationsStatus.COMPLETED)
            {
                var ObjRevision = db.QuoteRevisions.Where(r => r.QuoteId == Obj.Id).FirstOrDefault();
                if (ObjRevision == null) throw new QuotationException("Revision does not exist.");
                ObjRevision.RevisionNr = ObjRevision.RevisionNr + 1;
            }
                        
            db.SaveChanges();

            return Obj.Id;
        }
        public static int validateQuotation(int id)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            var Obj = db.Quotes.Find(id);

            Obj.QuoteStatusId = (int)DAL.Constants.QuotationsStatus.COMPLETED;

            db.SaveChanges();
            return Obj.Id;
        }


        public static int editQuotationsDraft(int key, DAL.DTO.Quote values)
        {

            DAL.Models.AISContext db = new DAL.Models.AISContext();

            var Obj = db.Quotes.FirstOrDefault(o => o.Id == key);
            if (Obj == null) throw new QuotationException("Quotation does not exist.");

            DTO.Quote changes = new DTO.Quote();
            var serializerSettings = new JsonSerializerSettings { ObjectCreationHandling = ObjectCreationHandling.Replace };
            JsonConvert.PopulateObject(JsonConvert.SerializeObject(values), changes, serializerSettings);

            if (changes.QuoteItems != null)
            {
                db.QuoteItems.RemoveRange(Obj.QuoteItems);
                Obj.QuoteItems.Clear();
            }

            JsonConvert.PopulateObject(JsonConvert.SerializeObject(values), Obj, serializerSettings);

            Obj.SubmissionDate = DateTime.Now;

            db.SaveChanges();

            return Obj.Id;
        }

        public static DAL.DTO.Quote GetEmailData(int id)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var source = db.Quotes
               .Select(p => new DAL.DTO.Quote
               {
                   Id = p.Id,
                   QuoteStatusId = p.QuoteStatusId,
                   QuoteStatusDisplay = p.QuoteStatus.Name,
                   RequestById = p.RequestById,
                   PlacedById = p.PlacedById,
                   SubmissionDate = p.SubmissionDate,
                   ValidFor = p.ValidFor,
                   DaysFrom = p.DaysFrom,
                   OnOrder = p.OnOrder,
                   OnDelivery = p.OnDelivery,
                   CustomerId = p.CustomerId,
                   PlacedByFullName = p.PlacedBy.Name + " - " + p.PlacedBy.Surname,
                   RequestByFullName = p.RequestBy.Name + " - " + p.RequestBy.Surname,
                   CompanyName = p.Customer.CompanyName,
                   QuoteItems = p.QuoteItems.Select(c => new DAL.DTO.QuoteItem
                   {
                       Id = c.Id,
                       ProductId = c.ProductId,
                       ProductName = c.Product.ProductCode + " - " + c.Product.ProductName,
                       Width = c.Width,
                       Length = c.Length,
                       Price = c.Price,
                       Quantity = c.Quantity,
                       QuoteId = c.QuoteId
                   }).ToList(),                   
                   QuoteTransports = p.QuoteTransports.Select(t => new DAL.DTO.QuoteTransport
                   {
                       Id = t.Id,
                       Description = t.Description,
                       Price = t.Price,
                       Quantity = t.Quantity,
                       QuoteId = t.QuoteId
                   }).ToList(),
               }).First(d => d.Id == id);
            return source;
        }

        public static string GetPlacedByRecipient(int id)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            int recipientId = (int)db.Quotes.Where(s => s.Id == id).Select(t => t.PlacedById).FirstOrDefault();
            string email = db.UserDetails.Where(i => i.UserId == recipientId).Select(e => e.Email).FirstOrDefault();
            return email;
        }

        public static string GetRecipient(int id)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            int recipientId = (int)db.Quotes.Where(s => s.Id == id).Select(t => t.RequestById).FirstOrDefault();
            string email = db.UserDetails.Where(i => i.UserId == recipientId).Select(e => e.Email).FirstOrDefault();
            return email;
        }
  
    }
}
