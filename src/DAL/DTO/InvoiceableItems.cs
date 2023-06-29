using System;
using System.Collections.Generic;

namespace DAL.DTO
{
    public class InvoiceableItems
    {
        public List<InvoiceableGrnItems> InvoiceableGrnItems { get; set; }
        public List<Service> InvoiceableServices { get; set; }
    }
}
