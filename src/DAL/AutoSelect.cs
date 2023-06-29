using NLog;
using System.Data;
using System.Linq;

namespace DAL
{
    public static class AutoSelect
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public static object getNewestSupplier()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            var id = db.Suppliers.OrderByDescending(x => x.Id).Take(1).FirstOrDefault().Id;
            return id;
        }

        public static object getNewestStockCategory()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            var id = db.StockCategories.OrderByDescending(x => x.Id).Take(1).FirstOrDefault().Id;
            return id;
        }

        public static object getNewestStockGroup()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            var id = db.StockGroups.OrderByDescending(x => x.Id).Take(1).FirstOrDefault().Id;
            return id;
        }

        public static object getNewestDepartment()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            var id = db.Departments.OrderByDescending(x => x.Id).Take(1).FirstOrDefault().Id;
            return id;
        }

        public static object getNewestLocation()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            var id = db.PlantLocations.OrderByDescending(x => x.Id).Take(1).FirstOrDefault().Id;
            return id;
        }

        public static object getNewestUom()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            var id = db.UnitOfMeasurements.OrderByDescending(x => x.Id).Take(1).FirstOrDefault().Id;
            return id;
        }

        public static object getNewestPaymentMethod()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            var id = db.PaymentMethods.OrderByDescending(x => x.Id).Take(1).FirstOrDefault().Id;
            return id;
        }
        public static object getNewestBankName()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            var id = db.BankNames.OrderByDescending(x => x.Id).Take(1).FirstOrDefault().Id;
            return id;
        }

        public static object getNewestCostType()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            var id = db.CostTypes.OrderByDescending(x => x.Id).Take(1).FirstOrDefault().Id;
            return id;
        }
        public static object getNewestStorageType()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            var id = db.StorageTypes.OrderByDescending(x => x.Id).Take(1).FirstOrDefault().Id;
            return id;
        }
        
    }
}
