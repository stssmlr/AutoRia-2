namespace Data.Entities
{
    public class FuelType
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // -------- navigation property ----------
        public ICollection<Car>? Cars { get; set; }
    }
}
