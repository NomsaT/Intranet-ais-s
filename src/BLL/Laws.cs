using System.Linq;

namespace BLL
{
    public static class Laws
    {
        public static IQueryable<DAL.DTO.Law> getLaws()
        {
            return DAL.Laws.getLaws();
        }
    }
}
