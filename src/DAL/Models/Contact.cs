using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class Contact
    {
        public int Id { get; set; }
        public string PersonName { get; set; }
        public string PersonPosition { get; set; }
        public string ContactNumber { get; set; }
        public string ContactEmail { get; set; }
        public string WorkLandlineNumber { get; set; }
        public int? SupplierId { get; set; }
        public int? ContactId { get; set; }

        public virtual Customer ContactNavigation { get; set; }
        public virtual Supplier Supplier { get; set; }
    }
}
