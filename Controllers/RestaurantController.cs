using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestaurantRaterMVC.Models.Restaurant;
using RestaurantRaterMVC.Services.Restaurant;

namespace RestaurantRaterMVC.Controllers
{
    public class RestaurantController : Controller
    {
        private IRestaurantService _service;
        public RestaurantController(IRestaurantService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Index() 
        {
            List<RestaurantListItem> restaurants = await _service.GetAllRestaurants();
            return View(restaurants);

        }

        public async Task<IActionResult> Create()
        {
            //The Create functionality on this controller will require two endpoints:
            //A GET endpoint to get the Create View, which contains the HTML form we'll use to create Restaurants
            return View();
        }
        
        //- A POST endpoint to take in the user-submitted data from the form and turn it into a C# data object which can be stored in a database

        [HttpPost]
        public async Task<IActionResult> Create(RestaurantCreate model)
        {
            if(!ModelState.IsValid)
                return View(model);
            await _service.CreateRestaurant(model);
            return RedirectToAction(nameof(Index));
        }
    }
}