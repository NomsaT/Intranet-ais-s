namespace BLL
{
    public static class DashboardStores
    {
        public static object getTodaysDeliveries()
        {
            return DAL.DashboardStores.getTodaysDeliveries();
        }

        public static object getUpcommingDeliveries()
        {
            return DAL.DashboardStores.getUpcommingDeliveries();
        }

        public static object getLateDeliveries()
        {
            return DAL.DashboardStores.getLateDeliveries();
        }

        public static object getDeliveryReminderEmailing(int id, string type)
        {
            BLL.Emailing.DeliveryReminderEmailing(id, type);
            return id;
        }        
    }
}
