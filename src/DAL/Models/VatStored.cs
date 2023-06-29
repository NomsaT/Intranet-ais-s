using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class VatStored
    {
        public int Id { get; set; }
        public int GlcodeId { get; set; }
        public decimal VatAmount { get; set; }

        public virtual Glcode Glcode { get; set; }
    }
}
