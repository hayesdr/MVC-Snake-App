using Assignment2.Models.Entities;

namespace Assignment2.Services
{
    //interface that connects repository to database
    public interface ISnakeRegionRepository
    {
        SnakeRegion? Read(int id);
        ICollection<SnakeRegion> ReadAll();
        SnakeRegion? Create(int snakeId, int regionId);
        void UpdateSnakeRegion(int Id, string RegionName);
        void Remove(int snakeId, int Id);
    }
}
