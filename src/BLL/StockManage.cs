namespace BLL
{
    public static class StockManage
    {

        public static int addStock(DAL.DTO.StockQuantity stock)
        {
            return DAL.StockManage.addStock(stock);
        }

        public static int transferStockDepartment(DAL.DTO.TransferStockDepartment stock)
        {
            return DAL.StockManage.transferStock(stock);
        }

        public static int transferStockLocation(DAL.DTO.TransferStockLocation stock)
        {
            return DAL.StockManage.transferStockLocation(stock);
        }

        public static int scannerTransferByDepartment(DAL.DTO.TransferStockDepartment stock)
        {
            return DAL.StockManage.scannerTransferByDepartment(stock);
        }

        public static string scannerTransferByLocation(DAL.DTO.TransferStockLocation stock)
        {
            return DAL.StockManage.scannerTransferByLocation(stock);
        }

        public static int removeStock(DAL.DTO.StockQuantity stock)
        {
            return DAL.StockManage.removeStock(stock);
        }

        public static int removeConsumeStock(DAL.DTO.StockQuantity stock)
        {
            return DAL.StockManage.removeConsumeStock(stock);
        }

        /* public static int setStock(DAL.DTO.StockQuantity stock)
         {
             return DAL.StockManage.setStock(stock);
         }*/

        public static object getStockPrice(int id)
        {
            return DAL.StockManage.getStockPrice(id);
        }

        public static object getStockUOM(int id)
        {
            return DAL.StockManage.getStockUOM(id);
        }

        public static object getMinThreshold(int id)
        {
            return DAL.StockManage.getMinThreshold(id);
        }

        public static bool CheckSKU(string code,int id)
        {
            return DAL.StockManage.CheckSKU(code,id);
        }
        
        public static DAL.DTO.StockRecipe getMixRecipe(int id)
        {
            return DAL.StockManage.getMixRecipe(id);
        }

        public static int mixStock(DAL.DTO.StockRecipe stock)
        {
            return DAL.StockManage.mixStock(stock);
        }

        public static DAL.DTO.Currency GetCurrency(int id)
        {
            return DAL.StockManage.getCurrency(id);
        }

        public static object getStockByBarcode(int id)
        {
            return DAL.StockManage.getStockByBarcode(id);
        }        
    }
}
