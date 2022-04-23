using Assignment2.Models.Entities;
using System.ComponentModel.DataAnnotations;
namespace Assignment2.Models
{
    public enum DangerLvl 
    {
        low, harmful, deadly
    }

    public class Snake
    {
        //Creates the Id as the key when the database is created
        [Key]
        public int Id { get; set; }
        //limits length of value to 32 char
        [StringLength(32)]
        public string Name { get; set; } = String.Empty;
        public string? Description { get; set; }
        public DangerLvl Level { get; set; }

        public ICollection<SnakeRegion> Regions { get; set; }
        = new List<SnakeRegion>();
    }
}
