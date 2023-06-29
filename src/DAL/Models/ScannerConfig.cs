using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class ScannerConfig
    {
        public int Id { get; set; }
        public string DeviceId { get; set; }
        public string DeviceName { get; set; }
        public int PlantLocationId { get; set; }

        public virtual PlantLocation PlantLocation { get; set; }
    }
}
