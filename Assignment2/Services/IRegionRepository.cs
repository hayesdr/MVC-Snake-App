using Assignment2.Models.Entities;


namespace Assignment2.Services;

public interface IRegionRepository
{
    Region Create(Region newRegion);

    Region? Read(int id);

    ICollection<Region> ReadAll();

    void Update(int oldId, Region Region);

    void Delete(int id);

}

