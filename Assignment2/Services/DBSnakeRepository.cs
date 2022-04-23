using Assignment2.Models;
using Microsoft.EntityFrameworkCore;

namespace Assignment2.Services
{
    //this class implements the crud methods allowing us to interact with the context
    public class DbSnakeRepository : ISnakeRepository
    {
        private ApplicationDbContext _db;
        //implementing repository
        public DbSnakeRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        //called in the HttpPost of the create method in SnakeController
        //adds a new snake to the database
        public Snake Create(Snake newSnake)
        {
            _db.Snakes.Add(newSnake);
            _db.SaveChanges();
            return newSnake;
         
        }
        //called in the HttpPost of the delete method in SnakeController
        //Deletes a snake
        public void Delete(int id)
        {
            var snakeToDelete = Read(id);
            _db.Snakes.Remove(snakeToDelete);
            _db.SaveChanges();
        }
        //retrieves a single snake based upon the id of a record
        public Snake? Read(int id)
        {
            return _db.Snakes
           .Include(s => s.Regions)
              .ThenInclude(sr => sr.Region)
           .FirstOrDefault(s => s.Id == id);
        }

        public ICollection<Snake> ReadAll()
        {
            return _db.Snakes.ToList();
        }

        /// <summary>
        /// //updates a record in the database based on id
        /// </summary>
        /// <param name="oldId"></param>
        /// <param name="snake"></param>
        public void Update(int oldId, Snake snake)
        {
            var snakeToUpdate = Read(oldId);
            snakeToUpdate.Name = snake.Name;
            snakeToUpdate.Description = snake.Description;
            snakeToUpdate.Level = snake.Level;
            _db.SaveChanges();
        }
    }
}
