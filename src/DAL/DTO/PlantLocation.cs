namespace DAL.DTO
{
    public class PlantLocation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int AddressId { get; set; }
        public int? DefaultStoreId { get; set; }
        public PlantLocationAddress Address { get; set; }

        public PlantLocation()
        {
            Address = new PlantLocationAddress();
        }
    }
}
