using System;
using System.Collections.Generic;

namespace DAL.DTO
{
    public class Invoice
    {
        public int Id { get; set; }
        public string InvoiceNumber { get; set; }
        public decimal Total { get; set; }
        public int InternalOrderId { get; set; }
        public DateTime DateCreated { get; set; }
        public List<InvoiceItem> InvoiceItems { get; set; }
        public List<InvoiceOnceOffItem> InvoiceOnceOffItems { get; set; }
        public List<InvoiceServiceItem> InvoiceServiceItems { get; set; }
        public bool AllItemsReceived { get; set; }
        public decimal ExpectedTotal { get; set; }
        public decimal? Vat { get; set; }
    }
}
