using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class Grn
    {
        public Grn()
        {
            Grnitems = new HashSet<Grnitem>();
            GrnonceOffItems = new HashSet<GrnonceOffItem>();
        }

        public int Id { get; set; }
        public string GrnNumber { get; set; }
        public decimal Total { get; set; }
        public int InternalOrderId { get; set; }
        public DateTime DateCreated { get; set; }
        public int? CapturerId { get; set; }
        public string Comment { get; set; }
        public int? EditorId { get; set; }

        public virtual User Capturer { get; set; }
        public virtual User Editor { get; set; }
        public virtual InternalOrder InternalOrder { get; set; }
        public virtual ICollection<Grnitem> Grnitems { get; set; }
        public virtual ICollection<GrnonceOffItem> GrnonceOffItems { get; set; }
    }
}
