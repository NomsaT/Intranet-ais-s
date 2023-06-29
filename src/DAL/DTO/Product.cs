namespace DAL.DTO
{
    public class Product
    {
        public int Id { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentFullName { get; set; }
        public string Description { get; set; }
        public string Barcode { get; set; }
        public decimal? Quantity { get; set; }

    }
}
