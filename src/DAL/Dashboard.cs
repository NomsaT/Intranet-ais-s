using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace DAL
{
    public static class Dashboard
    {
        private static string currencyEUR = "1";
        private static string currencyUSD = "1";
        private static string previousEUR;
        private static string previousUSD;


        public static string CurrencyUSD { get => currencyUSD; set => currencyUSD = value; }
        public static string CurrencyEUR { get => currencyEUR; set => currencyEUR = value; }
        public static string PreviousEUR { get => previousEUR; set => previousEUR = value; }
        public static string PreviousUSD { get => previousUSD; set => previousUSD = value; }

        private static bool validBirthday(DateTime birthday)
        {
            DateTime bday = new DateTime(DateTime.Now.Year, birthday.Month, birthday.Day);
            if (bday < DateTime.Now.Date)
            {
                bday = bday.AddYears(1);
            }

            if (bday >= DateTime.Now.Date && bday < DateTime.Now.Date.AddDays(40))
            {
                return true;
            }

            return false;
        }

        private static DateTime orderBirthday(DateTime birthday)
        {
            DateTime bday = new DateTime(DateTime.Now.Year, birthday.Month, birthday.Day);
            if (bday < DateTime.Now.Date)
            {
                bday = bday.AddYears(1);
            }

            return bday;
        }

        public static object getBirthdays()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var source = db.UserDetails.Where(a => a.User.IsDisabled == false)
               .Select(p => new DAL.DTO.FullEmployeeName
               {
                   FullName = p.User.Name + " " + p.User.Surname,
                   Birthday = p.Birthday
               }).ToList()
            .Where(d => validBirthday(d.Birthday))
            .OrderBy(b => orderBirthday(b.Birthday));
            return source;
        }

        public static object getFlaggedItems()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var source = db.PriceIncreases.OrderBy(d => d.Date)
               .Select(i => new DAL.DTO.FlaggedItems
               {
                   Id = i.Id,
                   Increase = i.Increase,
                   Date = i.Date,
                   Comment = i.Comment,
                   IncreaseType = i.IncreaseType.Type,
                   ProductCode = i.PriceLookUp.Stock.Code,
                   InternalProductName = i.PriceLookUp.Stock.InternalProductName,
                   CompanyName = i.PriceLookUp.Supplier.CompanyName,
                   IncreaseOrigin = i.IncreaseOrigin
               });
            return source;
        }

        public static int getPriceLookupBadge()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var source = db.PriceIncreases.Count();

            return source;
        }

        public static int getPrintingBadge()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var source = db.StockQuantities.Where(a => a.Stock.Monitored == true && a.BarcodePrinted == null).Count();
            return source;
        }

        public static object getWeeklyStockItems()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            var source = db.StockQuantities
               .Select(i => new DAL.DTO.WeeklyStockItems
               {
                   Id = i.Id,
                   Code = i.Stock.Code,
                   Date = i.DateCreated,
                   InternalProductName = i.Stock.InternalProductName,
                   UomType = i.Stock.Uom.Name,
                   Quantity = i.ItemQuantity,
                   StockCategory = i.Stock.StockCategory.Name
               })
               .OrderByDescending(d => d.Date).Take(5);
            return source;
        }

        public static int getTotalStockItems()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var source = db.Stocks.Count();
            return source;
        }

        public static object getMonthlyStockValue()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var source = db.Stocks
               .Select(p => new DAL.DTO.Stock
               {
                   Id = p.Id,
                   Code = p.Code,
                   ProductName = p.ProductName,
                   InternalSku = p.InternalSku,
                   InternalProductName = p.InternalProductName,
                   Uomid = p.Uomid,
                   StockCategoryId = p.StockCategoryId,
                   Quantity = p.StockQuantities.Sum(e => e.ItemQuantity),
                   /*Price = p.StockQuantities.Sum(e => (e.Price * e.ItemQuantity))*/
                   Price = p.StockQuantities.Where(i => i.Store.ProductionStore == false).Sum(e => (e.Price))

               });
            decimal totalprice = 0;

            foreach (var item in source)
            {
                totalprice += item.Price;
            }

            return totalprice;
        }

        public static object getMonthlyStockPercentage()
        {

            DAL.Models.AISContext db = new DAL.Models.AISContext();
            //get today's date
            DateTime curdate = DateTime.Now;
            //date from 30 days ago
            DateTime monthsDate = curdate.AddDays(-30);
            //two months back
            DateTime twoMonthsDate = curdate.AddDays(-60);


            //dates from a month ago( Between now and -30 days ago)
            decimal monthsDateSum = db.StockQuantities
               .Select(p => new DAL.DTO.StockQuantity
               {
                   Id = p.Id,
                   DateModified=p.DateModified,
                   Price=p.Price,
               }).Where(p => p.DateModified >= monthsDate && p.DateModified <= curdate).Sum(x=>x.Price);
            //dates between 30 and 60 days 
            decimal twoMonthsDateSum = db.StockQuantities
            .Select(p => new DAL.DTO.StockQuantity
            {
                Id = p.Id,
                DateModified = p.DateModified,
                Price = p.Price,
            }).Where(p => p.DateModified >= twoMonthsDate && p.DateModified <monthsDate).Sum(x => x.Price);

          decimal monthlyStockPercentage= twoMonthsDateSum > 0?(monthsDateSum - twoMonthsDateSum) / twoMonthsDateSum * 100:100;
          return Math.Round(monthlyStockPercentage, 2);
        }


        public static object getProductuctionStoreValue()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var source = db.StockQuantities
               .Select(p => new DAL.DTO.ProductionStore
               {
                   Id = p.Id,
                   isInProductionStore = p.Store.ProductionStore,
                   StoreId=p.StoreId, 
                   Price = p.Price
               }).Where(x=>(bool)x.isInProductionStore);
            decimal totalprice = 0;
            foreach (var item in source)
            {   
                totalprice += item.Price;   
            }
            return totalprice;
        }
        public static object getMinStockThreshold()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var source = db.Stocks
               .Select(p => new DAL.DTO.Stock
               {
                   Id = p.Id,
                   Code = p.Code,
                   InternalSku = p.InternalSku,
                   InternalProductName = p.InternalProductName,
                   Uomid = p.Uomid,
                   StockCategoryId = p.StockCategoryId,
                   MinThreshold = p.MinThreshold,
                   Quantity = p.StockQuantities.Sum(e => e.ItemQuantity),
                   Price = p.StockQuantities.Sum(e => (e.Price * e.ItemQuantity))

               }).Where(s => s.Quantity <= s.MinThreshold);
            return source;
        }

        public static int getTotalDepartments()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var source = db.Departments.Count();
            return source;
        }

        public static int getTotalLocations()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var source = db.PlantLocations.Count();
            return source;
        }

        public static object getDepartmentStock()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var source = db.Departments
               .Select(p => new DAL.DTO.DepartmentTotalStock
               {
                   Id = p.Id,
                   Name = p.Name,
                   Description = p.Description,
                   Color = p.Color,
                   TotalPrice = p.StockQuantities.Sum(e => (e.Price))
               }).Where(t => t.TotalPrice > 0);
            return source;
        }

        public static object getProductionStock() //gets non production and production stock
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var source = db.StockQuantities
            .Select(p => new DAL.DTO.ProductionStore
            {
                Id = p.Id,
                isInProductionStore = p.Store.ProductionStore,
                StoreId = p.StoreId,
                Price = p.Price
            }).GroupBy(x => (bool)x.isInProductionStore)
            .Select(y => new DAL.DTO.ProductionStore{ isInProductionStore = y.Key, Price = y.Sum(z => z.Price) })
            .ToList();
            return source;
        }

        public static object getOrders()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            var source = db.InternalOrders
               .Select(p => new DAL.DTO.InternalOrder
               {
                   Id = p.Id,
                   RequestedById = p.RequestedById,
                   RequestByFullName = p.RequestedBy.Name + " - " + p.RequestedBy.Surname,
                   PlacedById = p.PlacedById,
                   PlacedByFullName = p.PlacedBy.Name + " - " + p.PlacedBy.Surname,
                   QuotationNumber = p.QuotationNumber,
                   SupplierId = p.SupplierId,
                   SupplierFullName = p.Supplier.CompanyName,
                   ApproveById = p.ApproveById,
                   ApproveByFullName = p.ApproveBy.Name + " - " + p.ApproveBy.Surname,
                   Comment = p.Comment,
                   FollowUpDate = p.FollowUpDate,
                   DateCreated = p.DateCreated,
                   DeliveryDate = p.DeliveryDate,
                   Total = p.Total,
                   StatusId = p.StatusId,
                   StatusDisplay = p.Status.Name,
                   PlantLocationId = p.PlantLocationId,
                   PlantLocationName = p.PlantLocation.Name,
                   SupplierComment = p.SupplierComment,
                   SupplierCurrency = p.Supplier.Currency.Iso,
                   Vat = p.Vat,
                   ProjectId = p.ProjectId,
                   ProjectName = p.Project.Name,
                   CompanyId = p.CompanyId,
                   CompanyName = p.Company.Name,
                   internalOrderItems = p.InternalOrderItems.Select(c => new DAL.DTO.InternalOrderItem
                   {
                       Id = c.Id,
                       InternalOrderId = c.InternalOrderId,
                       StockId = c.StockId,
                       //Stock = c.Stock.Code + " - " + c.Stock.ProductName + " - " + c.Stock.InternalProductName,
                       StockName = c.Stock.ProductName + " - " + c.Stock.InternalProductName,
                       OriginalValue = c.OriginalValue,
                       Value = c.Value,
                       Quantity = c.Quantity,
                       GlcodeId = c.GlcodeId,
                       GlFullName = c.Glcode != null ? (c.Glcode.Code + " - " + c.Glcode.Name) : null,
                       Total = c.Total,
                       DepartmentId = c.DepartmentId,
                       DepartmentName = c.Department.Name,
                       VatAppl = c.VatAppl,
                       GrnAppl = c.GrnAppl,
                       Uomid = c.Uomid,
                       UomName = c.Stock.Uom.Name,
                       PackSize = c.PackSize,
                       UomPrice = c.UomPrice
                   }).ToList(),
                   onceOffItems = p.OnceOffItems.Select(f => new DAL.DTO.OnceOffItem
                   {
                       Id = f.Id,
                       Description = f.Description,
                       InternalOrderId = f.InternalOrderId,
                       Value = f.Value,
                       Quantity = f.Quantity,
                       DepartmentId = f.DepartmentId,
                       DepartmentName = f.Department.Name,
                       Uomid = f.Uomid,
                       UomName = f.Uom.Name,
                       PackSize = f.PackSize,
                       VatAppl = f.VatAppl,
                       GrnAppl = f.GrnAppl,
                       GlcodeId = f.GlcodeId,
                       Total = f.Quantity * f.Value,
                       TotalUnits = (int)(f.Quantity != null ? f.Quantity : 0) * (int)(f.PackSize != null ? f.PackSize : 0),
                       GlFullName = f.Glcode != null ? (f.Glcode.Code + " - " + f.Glcode.Name) : null,
                       Code = f.Code
                   }).ToList(),
                   services = p.Services.Select(s => new DAL.DTO.Service
                   {
                       Id = s.Id,
                       Description = s.Description,
                       InternalOrderId = s.InternalOrderId,
                       Value = s.Value,
                       VatAppl = s.VatAppl,
                       GrnAppl = s.GrnAppl,
                       GlcodeId = s.GlcodeId,
                       GlFullName = s.Glcode != null ? (s.Glcode.Code + " - " + s.Glcode.Name) : null,
                       DepartmentId = s.DepartmentId,
                       DepartmentName = s.Department.Name,
                       Quantity = s.Quantity,
                       Code = s.Code
                   }).ToList()
               }).Where(a => a.StatusId == (int)DAL.Constants.InternalOrderStatus.PENDING);
            return source;
        }

        public static object getFilteredOrders(int userId)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            var source = db.InternalOrders
               .Select(p => new DAL.DTO.InternalOrder
               {
                   Id = p.Id,
                   RequestedById = p.RequestedById,
                   RequestByFullName = p.RequestedBy.Name + " - " + p.RequestedBy.Surname,
                   PlacedById = p.PlacedById,
                   PlacedByFullName = p.PlacedBy.Name + " - " + p.PlacedBy.Surname,
                   QuotationNumber = p.QuotationNumber,
                   SupplierId = p.SupplierId,
                   SupplierFullName = p.Supplier.CompanyName,
                   ApproveById = p.ApproveById,
                   ApproveByFullName = p.ApproveBy.Name + " - " + p.ApproveBy.Surname,
                   Comment = p.Comment,
                   FollowUpDate = p.FollowUpDate,
                   DateCreated = p.DateCreated,
                   DeliveryDate = p.DeliveryDate,
                   Total = p.Total,
                   StatusId = p.StatusId,
                   StatusDisplay = p.Status.Name,
                   PlantLocationId = p.PlantLocationId,
                   PlantLocationName = p.PlantLocation.Name,
                   SupplierComment = p.SupplierComment,
                   SupplierCurrency = p.Supplier.Currency.Iso,
                   Vat = p.Vat,
                   ProjectId = p.ProjectId,
                   ProjectName = p.Project.Name,
                   CompanyId = p.CompanyId,
                   CompanyName = p.Company.Name,
                   internalOrderItems = p.InternalOrderItems.Select(c => new DAL.DTO.InternalOrderItem
                   {
                       Id = c.Id,
                       InternalOrderId = c.InternalOrderId,
                       StockId = c.StockId,
                       //Stock = c.Stock.Code + " - " + c.Stock.ProductName + " - " + c.Stock.InternalProductName,
                       StockName = c.Stock.ProductName + " - " + c.Stock.InternalProductName,
                       OriginalValue = c.OriginalValue,
                       Value = c.Value,
                       Quantity = c.Quantity,
                       GlcodeId = c.GlcodeId,
                       GlFullName = c.Glcode != null ? (c.Glcode.Code + " - " + c.Glcode.Name) : null,
                       Total = c.Total,
                       DepartmentId = c.DepartmentId,
                       DepartmentName = c.Department.Name,
                       VatAppl = c.VatAppl,
                       GrnAppl = c.GrnAppl,
                       Uomid = c.Uomid,
                       UomName = c.Stock.Uom.Name,
                       PackSize = c.PackSize,
                       UomPrice = c.UomPrice
                   }).ToList(),
                   onceOffItems = p.OnceOffItems.Select(f => new DAL.DTO.OnceOffItem
                   {
                       Id = f.Id,
                       Description = f.Description,
                       InternalOrderId = f.InternalOrderId,
                       Value = f.Value,
                       Quantity = f.Quantity,
                       DepartmentId = f.DepartmentId,
                       DepartmentName = f.Department.Name,
                       Uomid = f.Uomid,
                       UomName = f.Uom.Name,
                       PackSize = f.PackSize,
                       VatAppl = f.VatAppl,
                       GrnAppl = f.GrnAppl,
                       GlcodeId = f.GlcodeId,
                       Total = f.Quantity * f.Value,
                       TotalUnits = (int)(f.Quantity != null ? f.Quantity : 0) * (int)(f.PackSize != null ? f.PackSize : 0),
                       GlFullName = f.Glcode != null ? (f.Glcode.Code + " - " + f.Glcode.Name) : null,
                       Code = f.Code
                   }).ToList(),
                   services = p.Services.Select(s => new DAL.DTO.Service
                   {
                       Id = s.Id,
                       Description = s.Description,
                       InternalOrderId = s.InternalOrderId,
                       Value = s.Value,
                       VatAppl = s.VatAppl,
                       GrnAppl = s.GrnAppl,
                       GlcodeId = s.GlcodeId,
                       GlFullName = s.Glcode != null ? (s.Glcode.Code + " - " + s.Glcode.Name) : null,
                       DepartmentId = s.DepartmentId,
                       DepartmentName = s.Department.Name,
                       Quantity = s.Quantity,
                       Code = s.Code
                   }).ToList()
               }).Where(a => a.StatusId == (int)DAL.Constants.InternalOrderStatus.PENDING && a.ApproveById == userId);
            return source;
        }

        public static object getOrdersReview()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            var source = db.InternalOrders
               .Select(p => new DAL.DTO.InternalOrder
               {
                   Id = p.Id,
                   RequestedById = p.RequestedById,
                   RequestByFullName = p.RequestedBy.Name + " " + p.RequestedBy.Surname,
                   PlacedById = p.PlacedById,
                   PlacedByFullName = p.PlacedBy.Name + " " + p.PlacedBy.Surname,
                   QuotationNumber = p.QuotationNumber,
                   SupplierId = p.SupplierId,
                   SupplierFullName = p.Supplier.CompanyName,
                   ApproveById = p.ApproveById,
                   ApproveByFullName = p.ApproveBy.Name + " " + p.ApproveBy.Surname,
                   Comment = p.Comment,
                   FollowUpDate = p.FollowUpDate,
                   DateCreated = p.DateCreated,
                   DeliveryDate = p.DeliveryDate,
                   Total = p.Total,
                   StatusId = p.StatusId,
                   StatusDisplay = p.Status.Name,
                   PlantLocationId = p.PlantLocationId,
                   PlantLocationName = p.PlantLocation.Name,
                   SupplierComment = p.SupplierComment,
                   SupplierCurrency = p.Supplier.Currency.Iso,
                   InternalComment = p.InternalComment,
                   Vat = p.Vat,
                   ProjectId = p.ProjectId,
                   ProjectName = p.Project.Name,
                   CompanyId = p.CompanyId,
                   CompanyName = p.Company.Name,
                   internalOrderItems = p.InternalOrderItems.Select(c => new DAL.DTO.InternalOrderItem
                   {
                       Id = c.Id,
                       InternalOrderId = c.InternalOrderId,
                       StockId = c.StockId,
                       StockName = c.Stock.Code + " - " + c.Stock.ProductName + " - " + c.Stock.InternalProductName,
                       OriginalValue = c.OriginalValue,
                       Value = c.Value,
                       Quantity = c.Quantity,
                       GlcodeId = c.GlcodeId,
                       GlFullName = c.Glcode != null ? (c.Glcode.Code + " - " + c.Glcode.Name) : null,
                       Total = c.Total,
                       DepartmentId = c.DepartmentId,
                       DepartmentName = c.Department.Name,
                       VatAppl = c.VatAppl,
                       GrnAppl = c.GrnAppl,
                       Uomid = c.Uomid,
                       UomName = c.Stock.Uom.Name,
                       PackSize = c.PackSize,
                       UomPrice = c.UomPrice
                   }).ToList(),
                   onceOffItems = p.OnceOffItems.Select(f => new DAL.DTO.OnceOffItem
                   {
                       Id = f.Id,
                       Description = f.Description,
                       InternalOrderId = f.InternalOrderId,
                       Value = f.Value,
                       Quantity = f.Quantity,
                       DepartmentId = f.DepartmentId,
                       DepartmentName = f.Department.Name,
                       Uomid = f.Uomid,
                       UomName = f.Uom.Name,
                       PackSize = f.PackSize,
                       VatAppl = f.VatAppl,
                       GrnAppl = f.GrnAppl,
                       GlcodeId = f.GlcodeId,
                       Total = f.Quantity * f.Value,
                       GlFullName = f.Glcode != null ? (f.Glcode.Code + " - " + f.Glcode.Name) : null,
                       Code = f.Code
                   }).ToList(),
                   services = p.Services.Select(s => new DAL.DTO.Service
                   {
                       Id = s.Id,
                       Description = s.Description,
                       InternalOrderId = s.InternalOrderId,
                       Value = s.Value,
                       VatAppl = s.VatAppl,
                       GrnAppl = s.GrnAppl,
                       GlcodeId = s.GlcodeId,
                       GlFullName = s.Glcode != null ? (s.Glcode.Code + " - " + s.Glcode.Name) : null,
                       DepartmentId = s.DepartmentId,
                       DepartmentName = s.Department.Name,
                       Quantity = s.Quantity,
                       Code = s.Code
                   }).ToList()
               }).Where(a => a.StatusId == (int)DAL.Constants.InternalOrderStatus.REVIEW);
            return source;
        }

        public static object getFilteredOrdersReview(int userId)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            var source = db.InternalOrders
               .Select(p => new DAL.DTO.InternalOrder
               {
                   Id = p.Id,
                   RequestedById = p.RequestedById,
                   RequestByFullName = p.RequestedBy.Name + " " + p.RequestedBy.Surname,
                   PlacedById = p.PlacedById,
                   PlacedByFullName = p.PlacedBy.Name + " " + p.PlacedBy.Surname,
                   QuotationNumber = p.QuotationNumber,
                   SupplierId = p.SupplierId,
                   SupplierFullName = p.Supplier.CompanyName,
                   ApproveById = p.ApproveById,
                   ApproveByFullName = p.ApproveBy.Name + " " + p.ApproveBy.Surname,
                   Comment = p.Comment,
                   FollowUpDate = p.FollowUpDate,
                   DateCreated = p.DateCreated,
                   DeliveryDate = p.DeliveryDate,
                   Total = p.Total,
                   StatusId = p.StatusId,
                   StatusDisplay = p.Status.Name,
                   PlantLocationId = p.PlantLocationId,
                   PlantLocationName = p.PlantLocation.Name,
                   SupplierComment = p.SupplierComment,
                   Vat = p.Vat,
                   ProjectId = p.ProjectId,
                   ProjectName = p.Project.Name,
                   CompanyId = p.CompanyId,
                   CompanyName = p.Company.Name,
                   InternalComment = p.InternalComment,
                   internalOrderItems = p.InternalOrderItems.Select(c => new DAL.DTO.InternalOrderItem
                   {
                       Id = c.Id,
                       InternalOrderId = c.InternalOrderId,
                       StockId = c.StockId,
                       StockName = c.Stock.Code + " - " + c.Stock.ProductName + " - " + c.Stock.InternalProductName,
                       OriginalValue = c.OriginalValue,
                       Value = c.Value,
                       Quantity = c.Quantity,
                       GlcodeId = c.GlcodeId,
                       GlFullName = c.Glcode != null ? (c.Glcode.Code + " - " + c.Glcode.Name) : null,
                       Total = c.Total,
                       DepartmentId = c.DepartmentId,
                       DepartmentName = c.Department.Name,
                       VatAppl = c.VatAppl,
                       GrnAppl = c.GrnAppl,
                       Uomid = c.Uomid,
                       UomName = c.Stock.Uom.Name,
                       PackSize = c.PackSize,
                       UomPrice = c.UomPrice
                   }).ToList(),
                   onceOffItems = p.OnceOffItems.Select(f => new DAL.DTO.OnceOffItem
                   {
                       Id = f.Id,
                       Description = f.Description,
                       InternalOrderId = f.InternalOrderId,
                       Value = f.Value,
                       Quantity = f.Quantity,
                       DepartmentId = f.DepartmentId,
                       DepartmentName = f.Department.Name,
                       Uomid = f.Uomid,
                       UomName = f.Uom.Name,
                       PackSize = f.PackSize,
                       VatAppl = f.VatAppl,
                       GrnAppl = f.GrnAppl,
                       GlcodeId = f.GlcodeId,
                       Total = f.Quantity * f.Value,
                       TotalUnits = (int)(f.Quantity != null ? f.Quantity : 0) * (int)(f.PackSize != null ? f.PackSize : 0),
                       GlFullName = f.Glcode != null ? (f.Glcode.Code + " - " + f.Glcode.Name) : null,
                       Code = f.Code
                   }).ToList(),
                   services = p.Services.Select(s => new DAL.DTO.Service
                   {
                       Id = s.Id,
                       Description = s.Description,
                       InternalOrderId = s.InternalOrderId,
                       Value = s.Value,
                       VatAppl = s.VatAppl,
                       GrnAppl = s.GrnAppl,
                       GlcodeId = s.GlcodeId,
                       GlFullName = s.Glcode != null ? (s.Glcode.Code + " - " + s.Glcode.Name) : null,
                       DepartmentId = s.DepartmentId,
                       DepartmentName = s.Department.Name,
                       Quantity = s.Quantity,
                       Code = s.Code
                   }).ToList()
               }).Where(a => a.StatusId == (int)DAL.Constants.InternalOrderStatus.REVIEW && a.ApproveById == userId);
            return source;
        }
        public static IQueryable<DAL.DTO.Stocktake> getStocktake()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var source = db.Stocktakes
               .Select(p => new DAL.DTO.Stocktake
               {
                   Id = p.Id,
                   Stock = p.Stock.Code + " - " + p.Stock.InternalProductName,
                   Location = p.PlantLocation.Name,
                   Store = p.Store.Name,
                   CapturedQty = p.CapturedQty,
                   CurrentQty = p.CurrentQty,
                   Recount = p.Recount,
                   UserFullName = p.User.Name + " " + p.User.Surname
               }).Where(x => x.CapturedQty == 0);
            return source;
        }

        public static int getTotalApprovedOrders()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var source = db.InternalOrders.Where(i => i.StatusId == (int)DAL.Constants.InternalOrderStatus.APPROVED && i.DateApproved.Value.Month == DateTime.Now.Month && i.DateApproved.Value.Year == DateTime.Now.Year).Count();
            return source;
        }

        public static int getTotalPendingOrders()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var source = db.InternalOrders.Where(i => i.StatusId == (int)DAL.Constants.InternalOrderStatus.PENDING && i.DateCreated.Value.Month == DateTime.Now.Month && i.DateCreated.Value.Year == DateTime.Now.Year).Count();
            return source;
        }

        public static int getTotalPendingPriceOrders()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var source = db.InternalOrders.Where(i => i.StatusId == (int)DAL.Constants.InternalOrderStatus.PENDINGMONITOREDAPPROVAL && i.DateCreated.Value.Month == DateTime.Now.Month && i.DateCreated.Value.Year == DateTime.Now.Year).Count();
            return source;
        }

        public static int getTotalReviewOrders()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var source = db.InternalOrders.Where(i => i.StatusId == (int)DAL.Constants.InternalOrderStatus.REVIEW && i.DateCreated.Value.Month == DateTime.Now.Month && i.DateCreated.Value.Year == DateTime.Now.Year).Count();
            return source;
        }

        public static int getTotalDraftOrders()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var source = db.InternalOrders.Where(i => i.StatusId == (int)DAL.Constants.InternalOrderStatus.DRAFT && i.DateCreated.Value.Month == DateTime.Now.Month && i.DateCreated.Value.Year == DateTime.Now.Year).Count();
            return source;
        }

        public static List<int> GetDonutData()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var pending = db.InternalOrders.Where(i => i.StatusId == (int)DAL.Constants.InternalOrderStatus.PENDING).Count();
            var approved = db.InternalOrders.Where(i => i.StatusId == (int)DAL.Constants.InternalOrderStatus.APPROVED && i.DateApproved.Value.Month == DateTime.Now.Month && i.DateApproved.Value.Year == DateTime.Now.Year).Count();
            var monitored = db.InternalOrders.Where(i => i.StatusId == (int)DAL.Constants.InternalOrderStatus.PENDINGMONITOREDAPPROVAL && i.DateApproved.Value.Month == DateTime.Now.Month && i.DateApproved.Value.Year == DateTime.Now.Year).Count();
            var review = db.InternalOrders.Where(i => i.StatusId == (int)DAL.Constants.InternalOrderStatus.REVIEW && i.DateApproved.Value.Month == DateTime.Now.Month && i.DateApproved.Value.Year == DateTime.Now.Year).Count();
            var draft = db.InternalOrders.Where(i => i.StatusId == (int)DAL.Constants.InternalOrderStatus.DRAFT && i.DateApproved.Value.Month == DateTime.Now.Month && i.DateApproved.Value.Year == DateTime.Now.Year).Count();
            var closed = db.InternalOrders.Where(i => i.StatusId == (int)DAL.Constants.InternalOrderStatus.CLOSE && i.DateApproved.Value.Month == DateTime.Now.Month && i.DateApproved.Value.Year == DateTime.Now.Year).Count();
            var list = new List<int>();
            list.Add(pending);
            list.Add(approved);
            list.Add(monitored);
            list.Add(review);
            list.Add(draft);
            list.Add(closed);

            return list;
        }

        public static List<DTO.DepartmentContribution> getDepartmentContribution()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext(); //accessing context class
            var source = (from grn in db.Grns.AsEnumerable()
                          join IO in db.InternalOrders on grn.InternalOrderId equals IO.Id
                          join IOT in db.InternalOrderItems on IO.Id equals IOT.InternalOrderId
                          join Dep in db.Departments on IOT.DepartmentId equals Dep.Id
                          join OOI in db.OnceOffItems on IO.Id equals OOI.InternalOrderId
                          select new
                          {
                              ID = grn.Id,
                              GrnNumber = grn.GrnNumber,
                              InternalOrderID = IO.Id,
                              DepartmentName = Dep.Name + "(" +Dep.CostType.Abbreviation +")",
                              Total = IO.InternalOrderItems.Sum(x => x.Total) + OOI.Total,
                              Month = IO.DateCreated.Value.ToString("MMMM")
                          })
                          .GroupBy(x => x.Month)
                          .Distinct()
                          .Select(x => new DTO.DepartmentContribution
                          {
                              month = x.First().Month,
                              value = (decimal)x.Sum(s => s.Total)

                          })
                          .ToList();
            return source;
        }
        public static List<DAL.DTO.PurchaseValue> getPurchaseValue()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            var data = db.InternalOrders
                .Where(x => x.DateCreated.Value.Year == DateTime.Now.Year)
                .AsEnumerable()
                         .GroupBy(
                            x => new
                            {
                                Month = x.DateCreated.Value.ToString("MMMM"),
                            },
                            (key, g) =>
                                new DTO.PurchaseValue
                                {
                                    month = key.Month,
                                    value = (g.Select(s => s.InternalOrderItems.Sum(x => x.Value)).Sum() + (decimal)g.Select(s => s.OnceOffItems.Sum(o => o.Total)).Sum()),
                                })
                         .ToList();

            db.Departments.Select(s => s);

            return data;
        }
    }
}
