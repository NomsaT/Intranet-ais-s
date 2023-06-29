using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class Dbversion
    {
        public string Version { get; set; }
        public DateTime DateTime { get; set; }
    }
}
