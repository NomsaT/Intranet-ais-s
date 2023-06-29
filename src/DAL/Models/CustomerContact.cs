#nullable disable

namespace DAL.Models
{
    public partial class CustomerContact
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int ContactsId { get; set; }

        public virtual Contact Contacts { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
