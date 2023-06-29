using System.Collections.Generic;

namespace BLL
{
    public static class Dashboard
    {
        public static object getBirthdays()
        {
            return DAL.Dashboard.getBirthdays();
        }

        public static object getFlaggedItems()
        {
            return DAL.Dashboard.getFlaggedItems();
        }

        public static int getPriceLookupBadge()
        {
            return DAL.Dashboard.getPriceLookupBadge();
        }
        public static int getPrintingBadge()
        {
            return DAL.Dashboard.getPrintingBadge();
        }
        
        public static object getWeeklyStockItems()
        {
            return DAL.Dashboard.getWeeklyStockItems();
        }
        public static object getTotalStockItems()
        {
            return DAL.Dashboard.getTotalStockItems();
        }

        public static object getMonthlyStockValue()
        {
            return DAL.Dashboard.getMonthlyStockValue();
        }

        public static object getMonthlyStockPercentage()
        {
            return DAL.Dashboard.getMonthlyStockPercentage();
        }


        public static object getProductuctionStoreValue()
        {
            return DAL.Dashboard.getProductuctionStoreValue();
        }
        public static object getTotalDepartments()
        {
            return DAL.Dashboard.getTotalDepartments();
        }

        public static object getTotalLocations()
        {
            return DAL.Dashboard.getTotalLocations();
        }
        
        public static object getMinStockThreshold()
        {
            return DAL.Dashboard.getMinStockThreshold();
        }

        public static object getDepartmentStock()
        {
            return DAL.Dashboard.getDepartmentStock();
        }

        public static object getProductionStock()
        {
            return DAL.Dashboard.getProductionStock();
        }


        public static object getOrders()
        {
            return DAL.Dashboard.getOrders();
        }

        public static object getFilteredOrders(int userId)
        {
            return DAL.Dashboard.getFilteredOrders(userId);
        }

        public static object getOrdersReview()
        {
            return DAL.Dashboard.getOrdersReview();
        }

        public static object getFilteredOrdersReview(int userId)
        {
            return DAL.Dashboard.getFilteredOrdersReview(userId);
        }
        public static object getStocktake()
        {
            return DAL.Dashboard.getStocktake();
        }
        public static int getTotalApprovedOrders()
        {
            return DAL.Dashboard.getTotalApprovedOrders();
        }

        public static object getTotalPendingOrders()
        {
            return DAL.Dashboard.getTotalPendingOrders();
        }

        public static object getTotalPendingPriceOrders()
        {
            return DAL.Dashboard.getTotalPendingPriceOrders();
        }

        public static object getTotalReviewOrders()
        {
            return DAL.Dashboard.getTotalReviewOrders();
        }

        public static object getTotalDraftOrders()
        {
            return DAL.Dashboard.getTotalDraftOrders();
        }

        public static List<int> GetDonutData()
        { 
            return DAL.Dashboard.GetDonutData();
        }

        public static List<DAL.DTO.PurchaseValue> getPurchaseValue()
        {
            return DAL.Dashboard.getPurchaseValue();
        }
        public static List<DAL.DTO.DepartmentContribution> getDepartmentContribution()
        {
            return DAL.Dashboard.getDepartmentContribution();
        }
    }
}
