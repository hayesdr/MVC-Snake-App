using Assignment2.Models.Entities;
using Assignment2.Services;
using Microsoft.EntityFrameworkCore;

namespace CSCI3110CRUDManyToMany.Services;
//implements snakeregion repository methods
public class DbSnakeRegionRepository : ISnakeRegionRepository
{
    private readonly ApplicationDbContext _db;
    private readonly ISnakeRepository _snakeRepo;
    private readonly IRegionRepository _regionRepo;

    public DbSnakeRegionRepository(
        ApplicationDbContext db,
        ISnakeRepository snakeRepo, IRegionRepository regionRepo)
    {
        _db = db;
        _snakeRepo = snakeRepo;
        _regionRepo = regionRepo;
    }

    public SnakeRegion? Read(int id)
    {
        return _db.SnakeRegions
           .Include(scg => scg.Snake)
           .Include(scg => scg.Region)
           //gets region name for filtering later
           .Include(scg => scg.RegionName)
           .FirstOrDefault(scg => scg.Id == id);
    }

    public ICollection<SnakeRegion> ReadAll()
    {
        return _db.SnakeRegions
           .Include(scg => scg.Snake)
           .Include(scg => scg.Region)
           .ToList();
    }

    //Creates a new snake region based on ids andadds it to the db
    /// <summary>
    /// 
    /// </summary>
    /// <param name="snakeId"></param>
    /// <param name="regionId"></param>
    /// <returns></returns>
    public SnakeRegion? Create(int snakeId, int regionId)
    {
        var snake = _snakeRepo.Read(snakeId);
        if (snake == null)
        {
            // The snake was not found
            return null;
        }
        var region = _regionRepo.Read(regionId);
        if (region == null)
        {
            // The Region was not found
            return null;
        }
        var SnakeRegion = new SnakeRegion
        {
            Snake = snake,
            Region = region,
            RegionName = region.RegionName

        };
        //adds new snakeregion to respective snake and region 
        snake.Regions.Add(SnakeRegion);
        region.Snakes.Add(SnakeRegion);
        
        _db.SaveChanges();
        return SnakeRegion;
    }

    public void UpdateSnakeRegion(int Id, string RegionName)
    {
        var SnakeRegion = Read(Id);
        if (SnakeRegion != null)
        {
            SnakeRegion.RegionName = RegionName;
            _db.SaveChanges();
        }
    }
    //removes a paticular snakeregion from snake id and snakeregion id
    public void Remove(int snakeId, int Id)
    {
        var snake = _snakeRepo.Read(snakeId);

        var SnakeRegion = snake!.Regions
            .FirstOrDefault(scg => scg.Id == Id);
        var region = SnakeRegion!.Region;
        //removes snakeregion from respective region and snake
        snake!.Regions.Remove(SnakeRegion);
        region!.Snakes.Remove(SnakeRegion);
        _db.SaveChanges();
    }
}