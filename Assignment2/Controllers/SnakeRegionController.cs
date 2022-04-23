using Assignment2.Models.Entities.ViewModels;
using Assignment2.Services;
using Microsoft.AspNetCore.Mvc;
namespace Assignment2.Controllers;

public class SnakeRegionController : Controller
{
    //repository injections
    private readonly ISnakeRepository _SnakeRepo;
    private readonly IRegionRepository _RegionRepo;
    private readonly ISnakeRegionRepository _SnakeRegionRepo;

    public SnakeRegionController(
        ISnakeRepository SnakeRepo,
        IRegionRepository RegionRepo,
        ISnakeRegionRepository SnakeRegionRepo)
    {
        _SnakeRepo = SnakeRepo;
        _RegionRepo = RegionRepo;
        _SnakeRegionRepo = SnakeRegionRepo;
    }

    //takes snake and region id
    public IActionResult Create([Bind(Prefix = "id")] int snakeId, int RegionId)
    {
        //reads ids and returns related objects
        var snake = _SnakeRepo.Read(snakeId);
        if (snake == null)
        {
            return RedirectToAction("Index", "Snake");
        }
        var region = _RegionRepo.Read(RegionId);
        if (region == null)
        {
            return RedirectToAction("Details", "Snake", new { Id = snakeId });
        }

        var SnakeRegion = snake.Regions
            .SingleOrDefault(scg => scg.RegionId == RegionId);
        //Creates a new snake region and adds it to the database
        if (ModelState.IsValid)
        {
            _SnakeRegionRepo.Create(snakeId, RegionId);
            return RedirectToAction("Details", "Snake", new { Id = snakeId });
        }
        if (SnakeRegion != null)
        {
            return RedirectToAction("Details", "Snake", new { id = snakeId });
        }
        var SnakeRegionVM = new SnakeRegionVM
        {
            Snake = snake,
            Region = region
        };
        
        return View(SnakeRegionVM);
    }

    [HttpPost, ValidateAntiForgeryToken, ActionName("Create")]
    public IActionResult CreateConfirmed(int snakeId, int RegionId)
    {
        _SnakeRegionRepo.Create(snakeId, RegionId);
        return RedirectToAction("Details", "Snake", new { Id = snakeId });
    }

    //public IActionResult AssignRegion([Bind(Prefix = "id")] int snakeId, int RegionId)
    //{
    //    var Snake = _SnakeRepo.Read(snakeId);
    //    if (Snake == null)
    //    {
    //        return RedirectToAction("Index", "Snake");
    //    }
    //    var SnakeRegion = Snake.Regions
    //        .FirstOrDefault(scg => scg.RegionId == RegionId);
    //    if (SnakeRegion == null)
    //    {
    //        return RedirectToAction("Details", "Snake", new { Id = snakeId });
    //    }
    //    return View(SnakeRegion);
    //}

    //[HttpPost, ValidateAntiForgeryToken, ActionName("AssignRegion")]
    //public IActionResult AssignRegionConfirmed(
    //    string snakeId, int Id, string regionName)
    //{
    //    _SnakeRegionRepo.UpdateSnakeRegion(Id, regionName);
    //    return RedirectToAction("Details", "Snake");
    //}

    //gets snake and region id and removes related snake region
    public IActionResult Remove([Bind(Prefix = "id")] int snakeId, int regionId)
    {
        var Snake = _SnakeRepo.Read(snakeId);
        if (Snake == null)
        {
            return RedirectToAction("Index", "Snake");
        }
        //removes snake regions where regionids match
        var SnakeRegion = Snake.Regions
            .FirstOrDefault(scg => scg.RegionId == regionId);
        if (SnakeRegion == null)
        {
            return RedirectToAction("Details", "Snake", new { Id = snakeId });
        }
        
        return View(SnakeRegion);
    }

    [HttpPost, ValidateAntiForgeryToken, ActionName("Remove")]
    public IActionResult RemoveConfirmed(
        int snakeId, int Id)
    {
        _SnakeRegionRepo.Remove(snakeId, Id);
        return RedirectToAction("Details", "Snake", new { Id = snakeId });
    }
}
