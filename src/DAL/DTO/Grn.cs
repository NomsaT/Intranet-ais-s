using System;
using System.Collections.Generic;

namespace DAL.DTO
{
    public class Grn
    {
        public int Id { get; set; }
        public string GrnNumber { get; set; }
        public decimal Total { get; set; }
        public int InternalOrderId { get; set; }
        public DateTime DateCreated { get; set; }
        public List<Grnitem> GrnItems { get; set; }
        public List<GrnonceOffItem> GrnonceOffItems { get; set; }
        public bool AllItemsReceived { get; set; }
        public decimal ExpectedTotal { get; set; }
        public int? CapturerId { get; set; }
        public string Comment { get; set; }
        public int? EditorId { get; set; }
    }
}
