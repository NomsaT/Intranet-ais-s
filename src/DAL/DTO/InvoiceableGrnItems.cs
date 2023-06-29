using System;
using System.Collections.Generic;

namespace DAL.DTO
{
    public class InvoiceableGrnItems
    {
        public string GrnNumber { get; set; }
        public DateTime DateCreated { get; set; }
        public List<Grnitem> GrnItems { get; set; }
        public List<GrnonceOffItem> GrnonceOffItems { get; set; }
        public string Comment { get; set; }
    }
}
