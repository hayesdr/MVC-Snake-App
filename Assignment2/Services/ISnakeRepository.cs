using Assignment2.Models;

//this class interfaces with DB context and createes the methods it uses. So when we inherit we must inherit these methods

namespace Assignment2.Services
{
    public interface ISnakeRepository
    {
       
        Snake Create(Snake newSnake);

        Snake? Read(int id);

        ICollection<Snake> ReadAll();

        void Update(int oldId, Snake Snake);

        void Delete(int id);
    }

}
