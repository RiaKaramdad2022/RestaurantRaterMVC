using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestaurantRaterMVC.Services.Rating;

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

    }
}