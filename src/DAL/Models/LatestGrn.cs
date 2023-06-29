using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class LatestGrn
    {
        public int Id { get; set; }
        public int InternalOrderId { get; set; }
        public int GrnIncrement { get; set; }

        public virtual InternalOrder InternalOrder { get; set; }
    }
}
