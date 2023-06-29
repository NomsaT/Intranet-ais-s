namespace BLL
{
    public static class AutoSelect
    {       
        public static object getNewestSupplier()
        {
            return DAL.AutoSelect.getNewestSupplier();
        }

        public static object getNewestStockCategory()
        {
            return DAL.AutoSelect.getNewestStockCategory();
        }

        public static object getNewestStockGroup()
        {
            return DAL.AutoSelect.getNewestStockGroup();
        }

        public static object getNewestDepartment()
        {
            return DAL.AutoSelect.getNewestDepartment();
        }

        public static object getNewestLocation()
        {
            return DAL.AutoSelect.getNewestLocation();
        }

        public static object getNewestUom()
        {
            return DAL.AutoSelect.getNewestUom();
        }

        public static object getNewestPaymentMethod()
        {
            return DAL.AutoSelect.getNewestPaymentMethod();
        }

        public static object getNewestBankName()
        {
            return DAL.AutoSelect.getNewestBankName();
        }
        public static object getNewestCostType()
        {
            return DAL.AutoSelect.getNewestCostType();
        }
        public static object getNewestStorageType()
        {
            return DAL.AutoSelect.getNewestStorageType();
        }        
    }
}
