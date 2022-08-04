using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestaurantRaterMVC.Models;
using RestaurantRaterMVC.Models.Restaurant;
using RestaurantRaterMVC.Services.Rating;
using RestaurantRaterMVC.Services.Restaurant;

namespace RestaurantRaterMVC.Controllers
{
    public class RatingController : Controller
    {
        private readonly IRatingService _service;
        public RatingController(IRatingService service)
        {
            _service = service;
        }
        
        //The Index method for Ratings is pretty simple - we just need to get all of the ratings from the service, and return them in the View. 
        // The list can be empty but it won't be null.
        public async Task<IActionResult> Index()
        {
            var ratings = await _service.GetAllRatings();
            return View(ratings);
        }

        //In addition to the Index route, we can have a route that returns only Ratings tied to a particular Restaurant. 
        // This is probably more useful than being able to view an individual Rating.

        //Start by creating a GET method in the Rating controller that takes in a Restaurant Id - we'll just call this route "Restaurant"
        /*public async Task<IActionResult> Restaurant (int id)
        {
            var ratings = await _service.GetRatingsForRestaurant(id);

        } */

        public async Task<IActionResult> Create()
        {
            return View();
        }
        //If we want to include other information, like the name of the Restaurant, in this View, we can simply add a RestaurantService instance to our controller as well 
        private readonly IRatingService _Service;
        private readonly IRestaurantService _restaurantService;
        public RatingController(IRatingService service, IRestaurantService restaurantService)
        {
            _service = service;
            _restaurantService= restaurantService;
        }

       // [HttpPost]
    /* public async Task<IActionResult> Create (RatingCreate model)
        {
            if(!ModelState.IsValid)
                return View(ModelState);
            bool isRated = await _service.RateRestaurant(model);
            if(!isRated)
                return View(model);
            else
                return RedirectToAction(nameof(Restaurant), new {id = model.RestaurantId });
        } */

    }
}
