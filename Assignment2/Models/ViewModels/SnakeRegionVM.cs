using System.ComponentModel.DataAnnotations;

namespace Assignment2.Models.Entities.ViewModels
{
    public class SnakeRegionVM
    {
        public Snake? Snake { get; set; }
        public Region? Region { get; set; }
    }
}
