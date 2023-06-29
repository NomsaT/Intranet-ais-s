using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class PlantLocation
    {
        public PlantLocation()
        {
            InternalOrders = new HashSet<InternalOrder>();
            ScannerConfigs = new HashSet<ScannerConfig>();
            StockQuantities = new HashSet<StockQuantity>();
            Stocks = new HashSet<Stock>();
            Stocktakes = new HashSet<Stocktake>();
            Stores = new HashSet<Store>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int AddressId { get; set; }
        public int? DefaultStoreId { get; set; }

        public virtual Address Address { get; set; }
        public virtual Store DefaultStore { get; set; }
        public virtual ICollection<InternalOrder> InternalOrders { get; set; }
        public virtual ICollection<ScannerConfig> ScannerConfigs { get; set; }
        public virtual ICollection<StockQuantity> StockQuantities { get; set; }
        public virtual ICollection<Stock> Stocks { get; set; }
        public virtual ICollection<Stocktake> Stocktakes { get; set; }
        public virtual ICollection<Store> Stores { get; set; }
    }
}
