using System.Collections.Generic;

namespace DAL.DTO
{
    public class StockRecipe
    {
        public int StockId { get; set; }
        public List<Recipe> Recipe { get; set; }

    }
}
