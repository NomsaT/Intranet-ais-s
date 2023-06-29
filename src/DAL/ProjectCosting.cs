using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;

namespace DAL
{
    public static class ProjectCosting
    {
        public static IQueryable<DAL.DTO.ProjectCosting> getProjectCosting()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var source = db.Projects
               .Select(p => new DAL.DTO.ProjectCosting
               {
                   Id = p.Id,
                   Name = p.Name,
                   StartDate = p.StartDate,
                   EndDate = p.EndDate,
                   ProjectStatusId = p.ProjectStatusId,
                   Budget = p.Budget,
                   AllInternalOrderTotals = (decimal)((p.InternalOrders.FirstOrDefault(i => i.ProjectId == p.Id) != null) ? p.InternalOrders.Where(i => i.ProjectId == p.Id && i.StatusId == (int)DAL.Constants.InternalOrderStatus.APPROVED).Sum(e => e.Total + e.Vat) : 0),
                   Difference = p.Budget - ((decimal)((p.InternalOrders.FirstOrDefault(i => i.ProjectId == p.Id) != null) ? p.InternalOrders.Where(i => i.ProjectId == p.Id && i.StatusId == (int)DAL.Constants.InternalOrderStatus.APPROVED).Sum(e => e.Total + e.Vat) : 0)),
                   DaysLeft = (p.EndDate - System.DateTime.Now).Days
               });
            return source;
        }
    }
}
