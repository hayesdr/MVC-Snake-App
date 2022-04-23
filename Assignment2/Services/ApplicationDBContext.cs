using Microsoft.EntityFrameworkCore;
using Assignment2.Models;
using Assignment2.Models.Entities;

namespace Assignment2.Services;

//this class allows us to interact with the database by creating a "context" of the database, used to execute queries and persist objects to the database
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    //return a collection of snake entities
    public DbSet<Snake> Snakes => Set<Snake>();
    public DbSet<Region> Regions => Set<Region>();
    public DbSet<SnakeRegion> SnakeRegions => Set<SnakeRegion>();
      
}

