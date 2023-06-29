#nullable disable

namespace DAL.Models
{
    public partial class SupplierContact
    {
        public int Id { get; set; }
        public int SupplierId { get; set; }
        public int ContactsId { get; set; }

        public virtual Contact Contacts { get; set; }
        public virtual Supplier Supplier { get; set; }
    }
}
