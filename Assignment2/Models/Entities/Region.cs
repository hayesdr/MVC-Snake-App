namespace Assignment2.Models.Entities
{
    public class Region
    {
        public int RegionId { get; set; }
        public string RegionName { get; set; } = string.Empty;
        public ICollection<SnakeRegion> Snakes { get; set; }
        = new List<SnakeRegion>();
    }
}
