using System.Linq;

namespace DAL
{
    public static class PriceIncrease
    {
        public static int addPriceIncrease(DAL.DTO.PriceIncrease increase)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            var DBObj = new DAL.Models.PriceIncrease
            {
                Increase = increase.Increase,
                Date = increase.Date,
                Comment = (increase.Comment != null) ? increase.Comment : "",
                IncreaseTypeId = increase.IncreaseTypeId,
                PriceLookUpId = increase.PriceLookUpId,
                IncreaseOrigin = "via price-lookup"

            };

            db.PriceIncreases.Add(DBObj);
            db.SaveChanges();
            return DBObj.Id;
        }

        public static int editPriceIncrease(DAL.DTO.PriceIncrease increase)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var Obj = db.PriceIncreases.FirstOrDefault(s => s.PriceLookUpId == increase.PriceLookUpId);
            if (Obj == null) throw new PriceIncreaseException("Price Increase does not exist.");

            Obj.Increase = increase.Increase;
            Obj.Date = increase.Date;
            Obj.Comment = (increase.Comment != null) ? increase.Comment : "";
            Obj.IncreaseTypeId = increase.IncreaseTypeId;

            db.SaveChanges();
            return Obj.Id;
        }

        public static object getItemInfo(int id)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            var source = db.PriceIncreases.Where(s => s.PriceLookUpId == id)
               .Select(i => new DAL.DTO.PriceIncrease
               {
                   Id = i.Id,
                   Increase = i.Increase,
                   Date = i.Date,
                   Comment = i.Comment,
                   IncreaseTypeId = i.IncreaseType.Id,
                   PriceLookUpId = i.PriceLookUp.Id,
                   IncreaseTypeName = i.IncreaseType.Type,
                   IncreaseOrigin = i.IncreaseOrigin
               });
            return source;
        }

        public static bool removeItemInfo(int id)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            var Obj = db.PriceIncreases.FirstOrDefault(r => r.Id == id);
            if (Obj == null) return false;

            db.PriceIncreases.Remove(Obj);
            db.SaveChanges();
            return true;
        }
    }
}
