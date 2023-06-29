namespace BLL
{
    public static class PriceIncrease
    {
        public static int addPriceIncrease(DAL.DTO.PriceIncrease increase)
        {
            return DAL.PriceIncrease.addPriceIncrease(increase);
        }

        public static int editPriceIncrease(DAL.DTO.PriceIncrease increase)
        {
            return DAL.PriceIncrease.editPriceIncrease(increase);
        }

        public static object getItemInfo(int id)
        {
            return DAL.PriceIncrease.getItemInfo(id);
        }

        public static bool removeItemInfo(int id)
        {
            return DAL.PriceIncrease.removeItemInfo(id);
        }

    }
}
