namespace DAL.DTO
{
    public class Bins
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Barcode { get; set; }
        public int PlantLocationId { get; set; }
        public string PlantLocationName { get; set; }
        public int StoreId { get; set; }
        public string StoreName { get; set; }
    }
}
