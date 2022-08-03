using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RestaurantRaterMVC.Data;

namespace RestaurantRaterMVC.Data
{
    //we created a database context using DbContext, a class we should now have access to via EF Core. This is the C# representation of our database.
    public class RestaurantDbContext : DbContext
    {
        //This will also need a constructor that inherits from the base (DbContext) controller, which will take in a DbContextOptions object tied to our RestaurantDbContext, 
        //and which needs to be passed into the base constructor.
        public RestaurantDbContext(DbContextOptions<RestaurantDbContext> options) : base(options) {}

        public DbSet<RestaurantEntity> Restaurants { get; set; }
        public DbSet<RatingEntity> Ratings { get; set; }
    }
}