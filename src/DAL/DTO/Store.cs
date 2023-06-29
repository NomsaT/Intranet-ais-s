namespace DAL.DTO
{
    public class Store
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int PlantLocationId { get; set; }
        public string PlantLocation { get; set; }
        public int StoreTypeId { get; set; }
        public bool? Quarantine { get; set; }
        public bool? ProductionStore { get; set; }
    }
}
