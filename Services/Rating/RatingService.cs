using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RestaurantRaterMVC.Data;
using RestaurantRaterMVC.Models;

namespace RestaurantRaterMVC.Services.Rating
{
    public class RatingService : IRatingService
    {
        private readonly RestaurantDbContext _context;
        public RatingService(RestaurantDbContext context)
        {
            _context = context;
        }
        public async Task<List<RatingListItem>> GetAllRatings()
        {
            // In the GetAllRatings method, all we need to do is convert the DbSet of Ratings to a list of RatingListItems. We can do this using the LINQ Select method:
            var ratings = _context.Ratings
            .Select(r => new RatingListItem()
            {
                Id = r.Id,
                RestaurantName = r.Restaurant.Name,
                FoodScore = r.FoodScore,
                AtmosphereScore = r.AtmosphereScore,
                CleanlinessScore = r.CleanlinessScore,
            });
            //This will give us an IQueryable, so to convert this to a list, we should call ToListAsync, then await this action and return it
            return await ratings.ToListAsync();
        }

        
    }
}
