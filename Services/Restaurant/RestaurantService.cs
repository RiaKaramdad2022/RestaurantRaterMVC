using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestaurantRaterMVC.Data;
using Microsoft.EntityFrameworkCore;
using RestaurantRaterMVC.Models.Restaurant;

namespace RestaurantRaterMVC.Services.Restaurant
{
    public class RestaurantService : IRestaurantService
    {
        //add a field to hold the RestaurantDb context, and inject it through the constructor
        private RestaurantDbContext _context;
        public RestaurantService(RestaurantDbContext context)
        {

            _context = context;
        }

        public async Task<List<RestaurantListItem>> GetAllRestaurants()
        {
            List<RestaurantListItem> restaurants = await _context.Restaurants
            .Include(r => r.Ratings)
            .Select(r => new RestaurantListItem()
            {
                Id = r.Id,
                Name = r.Name,
                Score = r.Score,
            }).ToListAsync();
            return restaurants;
        }

        /*List<Restaurant> restaurants = await _context.Restaurants.ToListAsync();
        foreach (Restaurant restaurant in restaurants){
            restaurant.Ratings = await _context.Ratings.Where(r => r.RestaurantId == restaurant.Id).ToListAsync();
        }
        List<RestaurantListItem> restaurantList = restaurants.Select(r => new RestaurantListItem()
        {
            Id = r.Id,
            Name = r.Name,
            Score = r.Score,
        }).ToListAsync();
            return restaurants; */

        public async Task<bool> CreateRestaurant(RestaurantCreate model)
        {
            RestaurantEntity restaurant = new RestaurantEntity()
            {
                Name = model.Name,
                Location = model.Location,
            };
            _context.Restaurants.Add(restaurant);
            return await _context.SaveChangesAsync() == 1;
        }
    }
}