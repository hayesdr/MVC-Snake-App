using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Assignment2.Models;
using Assignment2.Services;
using Microsoft.EntityFrameworkCore;


namespace Assignment2.Controllers
{
    public class SnakeController : Controller
    {
        //inject the repository

        private readonly ISnakeRepository _snakeRepo;
        private readonly ISnakeRegionRepository _snakeRegionRepo;
        public SnakeController(ISnakeRepository SnakeRepo, ISnakeRegionRepository SnakeRegionRepo)
        {
            _snakeRegionRepo = SnakeRegionRepo;
            _snakeRepo = SnakeRepo;
        }

        //returns all snakes

        public ActionResult List()
        {
            return View(_snakeRepo.ReadAll());
        }

        
        //input in index view sends searchString

        public IActionResult Index(string searchString)
        {
            //get all snakes
            var allSnakes = _snakeRepo.ReadAll();
            //Compare searchString to all snakes descr and name
            var results = allSnakes.
                Where(s => s.Name.Contains(searchString) || s.Description.Contains(searchString));
            //return index if no search
            if (searchString != null)
            {
                return View(results);
            }
            //return 
            return View(allSnakes);
        }

        //returns properties for a specific snake
        public ActionResult Details(int id)
        {
            var snake = _snakeRepo.Read(id);
            return View(snake);
        }

        //returns view for create action
        public ActionResult Create()
        {
            return View();
        }

        //Creates a new snake and adds to db
        
        [HttpPost]
        public IActionResult Create(Snake newSnake)
        {
            if (ModelState.IsValid)
            {
                _snakeRepo.Create(newSnake);
                return RedirectToAction("Index");
            }
            return View(newSnake);
        }

        //New Linq Feature
        //Checks the db for snakes whos regions are in the filter string

        public IActionResult RegionSort(string regionString)
        {
            var allSnakes = _snakeRepo.ReadAll();

            List<Snake> filteredSnakes = new List<Snake>();
            //gets all snakeregion objects
            var allSnakeRegions = _snakeRegionRepo.ReadAll();
            //filters snakeregions to snakeregions containing the regoin name
            var filteredSnakeRegions = allSnakeRegions.Where(sr => sr.RegionName.Contains(regionString));
            foreach (var snakeregion in filteredSnakeRegions)
            {
                //add snakes to list of snakes from filtered snake regions
                filteredSnakes.Add(snakeregion.Snake);
            }
            if (regionString != null)
            {
                return View(filteredSnakes);
            }

            return View(allSnakes);
        }
        //presents an update form similar to the create form. Uses the id to update the db

        public IActionResult Edit(int id)
        {
            var snake = _snakeRepo.Read(id);
            if (snake == null)
            {
                return RedirectToAction("Index");
            }
            return View(snake);
        }

        //updates a row in the database, for snake here

        [HttpPost]
        public IActionResult Edit(Snake snake)
        {
            if (ModelState.IsValid)
            {
                _snakeRepo.Update(snake.Id, snake);
                return RedirectToAction("Index");
            }
            return View(snake);
        }

        //deletes a snake row from the db
        public IActionResult Delete(int id)
        {
            var snake = _snakeRepo.Read(id);
            if (snake == null)
            {
                return RedirectToAction("Index");
            }
            return View(snake);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _snakeRepo.Delete(id);
            return RedirectToAction("Index");
        }
        
        
    }
}
