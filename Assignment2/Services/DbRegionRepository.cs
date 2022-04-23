using Assignment2.Models.Entities;
using Assignment2.Services;
using Microsoft.EntityFrameworkCore;

namespace Assignment2.Services;

public class DbRegionRepository : IRegionRepository
{
    private ApplicationDbContext _db;
    //implementing repository
    public DbRegionRepository(ApplicationDbContext db)
    {
        _db = db;
    }
    //called in the HttpPost of the create method in RegionController
    //adds a new Region to the database
    public Region Create(Region newRegion)
    {
        _db.Regions.Add(newRegion);
        _db.SaveChanges();
        return newRegion;

    }
    //called in the HttpPost of the delete method in RegionController
    //Deletes a Region
    public void Delete(int id)
    {
        var RegionToDelete = Read(id);
        _db.Regions.Remove(RegionToDelete);
        _db.SaveChanges();
    }
    //retrieves a single Region based upon the id of a record
    public Region? Read(int id)
    {
        return _db.Regions
           .Include(s => s.Snakes)
              .ThenInclude(sr => sr.Snake)
           .FirstOrDefault(s => s.RegionId == id);
    }

    public ICollection<Region> ReadAll()
    {
        return _db.Regions.ToList();
    }

    /// <summary>
    /// //updates a record in the database based on id
    /// </summary>
    /// <param name="oldId"></param>
    /// <param name="Region"></param>
    public void Update(int oldId, Region Region)
    {
        var RegionToUpdate = Read(oldId);
        RegionToUpdate.RegionName = Region.RegionName;
        RegionToUpdate.RegionId = Region.RegionId;
        _db.SaveChanges();
    }
}


