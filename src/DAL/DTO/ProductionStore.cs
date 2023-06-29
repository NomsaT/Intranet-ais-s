using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO
{
    internal class ProductionStore
    {

        public int Id { get; set; }
        public int StockId { get; set; }
        public decimal Price { get; set; }
        public int StoreId { get; set; }
        public bool? isInProductionStore{ get; set; }

    }
}
