using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class SupplierType
    {
        public SupplierType()
        {
            Suppliers = new HashSet<Supplier>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Supplier> Suppliers { get; set; }
    }
}
