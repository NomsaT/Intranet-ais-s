
namespace DAL
{
    public class Constants
    {
        //TODO: RECIPE Update Stock Category
        public enum StockCategory
        {
            //RECIPE = 2013
            RECIPE = 1009
        }

        public enum InternalOrderStatus
        {
            APPROVED = 1,
            PENDING = 2,
            DENIED = 3,
            PENDINGMONITOREDAPPROVAL = 4,
            REVIEW = 5,
            DRAFT = 6,
            CLOSE = 7
        }

        public enum LocationTypes
        {
            LOCAL = 1,
            INTERNATIONAL = 2
        }

        public enum IncreaseTypes
        {
            CURRENCY = 1,
            PERC = 2,
            NEWPRICE = 3
        }

        public enum InvoiceAction
        {
            ADD = 1,
            UPDATE = 2,
            REMOVE = 3
        }

        public enum GrnAction
        {
            ADD = 1,
            UPDATE = 2,
            REMOVE = 3
        }

        public enum InternalOrder
        {
            ADD = 1,
            UPDATE = 2
        }

        public enum Permissions
        {
            APPROVEINCREASES = 100,
            INVOICEINCREASES = 104
        }

        public enum Quotations
        {
            ADD = 1,
            UPDATE = 2
        }

        public enum QuotationsStatus
        {
            COMPLETED = 1,
            DRAFT = 2
        }

        public enum ProjectStatus
        {
            OPEN = 1,
            CLOSE = 2
        }
    }
}
