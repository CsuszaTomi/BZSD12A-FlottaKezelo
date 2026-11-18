namespace BZSD12A.Models
{
    internal class Car
    {
        public int Id { get; set; }
        public string PlateNumber { get; set; } = string.Empty;
        public string ChassisNumber { get; set; } = string.Empty;
        public int BrandId { get; set; }
        public int ModelId { get; set; }
        public int UserId { get; set; }
        public DateTime CarYear { get; set; }
        public string Consumption { get; set; } = string.Empty;   // pl. "5.4 l/100km"
        public string RanKm { get; set; } = string.Empty;
        public string FuelType { get; set; } = string.Empty;

        // Csak megjelenítéshez (JOIN-ból jönnek)
        public string BrandName { get; set; } = string.Empty;
        public string ModelName { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
    }
}