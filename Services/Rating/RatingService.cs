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

        public async Task<List<RatingListItem>> GetRatingsForRestaurant(int id)
        {
            var ratings = _context.Ratings
            .Where(r => r.RestaurantId == id)
            .Select(r => new RatingListItem()
            {
                Id = r.Id,
                RestaurantName = r.Restaurant.Name,
                FoodScore = r.FoodScore,
                AtmosphereScore = r.AtmosphereScore,
                CleanlinessScore = r.CleanlinessScore,
            });
            return await ratings.ToListAsync();
        }

        //To rate a Restaurant, we just need to take a RatingCreate model, and convert it into a RatingEntity for the database:
        public async Task<bool> RateRestaurant(RatingCreate model)
        {
            var rating = new RatingEntity()
            {
                FoodScore = model.FoodScore,
                AtmosphereScore = model.AtmosphereScore,
                CleanlinessScore = model.CleanlinessScore,
                RestaurantId = model.RestaurantId,
            };
            // And then we can add that entity to the context and save
            _context.Ratings.Add(rating);
            return await _context.SaveChangesAsync() == 1;
        }
    }
    }
