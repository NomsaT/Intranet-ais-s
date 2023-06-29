using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class BarcodeTemp
    {
        public int Id { get; set; }
        public string Barcode { get; set; }
        public string Template { get; set; }
    }
}
