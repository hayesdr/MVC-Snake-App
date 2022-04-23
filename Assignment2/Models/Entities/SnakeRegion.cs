using Assignment2.Models;
using Assignment2.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace Assignment2.Models.Entities;

public class SnakeRegion
{
    public int Id { get; set; }
    
    public string RegionName { get; set; } = String.Empty;

    public int SnakeId { get; set; }
    public Snake? Snake { get; set; }

    public int RegionId { get; set; }
    public Region? Region { get; set; }
}


