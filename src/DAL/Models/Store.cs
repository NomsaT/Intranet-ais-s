using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class Store
    {
        public Store()
        {
            Bins = new HashSet<Bin>();
            Grnitems = new HashSet<Grnitem>();
            PlantLocations = new HashSet<PlantLocation>();
            StockQuantities = new HashSet<StockQuantity>();
            Stocks = new HashSet<Stock>();
            Stocktakes = new HashSet<Stocktake>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int PlantLocationId { get; set; }
        public int StoreTypeId { get; set; }
        public bool? Quarantine { get; set; }
        public bool? ProductionStore { get; set; }

        public virtual PlantLocation PlantLocation { get; set; }
        public virtual StoreType StoreType { get; set; }
        public virtual ICollection<Bin> Bins { get; set; }
        public virtual ICollection<Grnitem> Grnitems { get; set; }
        public virtual ICollection<PlantLocation> PlantLocations { get; set; }
        public virtual ICollection<StockQuantity> StockQuantities { get; set; }
        public virtual ICollection<Stock> Stocks { get; set; }
        public virtual ICollection<Stocktake> Stocktakes { get; set; }
    }
}
