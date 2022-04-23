using Assignment2.Models.Entities;
using Assignment2.Models.Entities.ViewModels;
using Assignment2.Services;

using Microsoft.AspNetCore.Mvc;

namespace Assignment2.Controllers
{
    public class RegionController : Controller
    {
        //inject the repository
        private readonly IRegionRepository _RegionRepo;
        private readonly ISnakeRepository _snakeRepo;
        public RegionController(IRegionRepository RegionRepo, ISnakeRepository snakeRepo)
        {
            _snakeRepo = snakeRepo;
            var snakes = _snakeRepo.ReadAll();
            _RegionRepo = RegionRepo;
            var regions = _RegionRepo.ReadAll();
        }

        //Gets all snkes and returns their data
        public ActionResult Index()
        {
            return View(_RegionRepo.ReadAll());
        }

        //Gets details of a region
        public ActionResult Details(int id)
        {
            var Region = _RegionRepo.Read(id);
            return View(Region);
        }

        //creates a region 
        public ActionResult Create()
        {
            return View();
        }

        //Creates a new Region and adds to db
        // POST: HomeController3/Create
        [HttpPost]
        public IActionResult Create(Region newRegion)
        {
            if (ModelState.IsValid)
            {
                _RegionRepo.Create(newRegion);
                return RedirectToAction("Index");
            }
            return View(newRegion);
        }

        //presents an update form similar to the create form. Uses the id to update the db
        public IActionResult Edit(int id)
        {
            var Region = _RegionRepo.Read(id);
            if (Region == null)
            {
                return RedirectToAction("Index");
            }
            return View(Region);
        }

        //updates a row in the database
        [HttpPost]
        public IActionResult Edit(Region Region)
        {
            if (ModelState.IsValid)
            {
                _RegionRepo.Update(Region.RegionId, Region);
                return RedirectToAction("Index");
            }
            return View(Region);
        }

        // GET: HomeController3/Delete/5
        public IActionResult Delete(int id)
        {
            var Region = _RegionRepo.Read(id);
            if (Region == null)
            {
                return RedirectToAction("Index");
            }
            return View(Region);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _RegionRepo.Delete(id);
            return RedirectToAction("Index");
        }
        //gets the regions a snake is registered to and returns all regions but the ones a snake already contains
        public IActionResult Register([Bind(Prefix = "Id")] int snakeId)
        {
            var snake = _snakeRepo.Read(snakeId);
            if (snake == null)
            {
                return RedirectToAction("Index", "Snake");
            }
            var allRegions = _RegionRepo.ReadAll();
            //filters regions for regions snake doesnt have
            var regionsRegistered = snake.Regions
                .Select(scg => scg.Region).ToList();
            var RegionsNotRegistered = allRegions.Except(regionsRegistered);
            ViewData["Snake"] = snake;
            //returns regions snake is not in
            return View(RegionsNotRegistered);
        }
    }
}
