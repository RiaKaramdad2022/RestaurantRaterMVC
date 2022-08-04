using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestaurantRaterMVC.Data;
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

        public async Task<IActionResult> Details(int id)
        {
            RestaurantDetail restaurant = await _service.GetRestaurantById(id);
            if(restaurant == null) return RedirectToAction(nameof(Index));
            return View(restaurant);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var restaurant = await _service.GetRestaurantById(id);
            if(restaurant == null)
            {
                return RedirectToAction(nameof(Index));
            }
            RestaurantEdit restaurantEdit = new RestaurantEdit()
            {
                Id= restaurant.Id,
                Name = restaurant.Name,
                Location = restaurant.Location,
            };
            return View(restaurantEdit);
        }
        

        [HttpPost]
        public async Task<IActionResult> Edit (int id, RestaurantEdit model)
        {
            //Here, we need to do four things - first, if the ModelState is not valid (for instance, if the Name is too long, or empty), then we should return the 
            //view with the ModelState - this way, we can return to the view with error messages/warnings about the invalid input.
            if(!ModelState.IsValid) return View(ModelState);
            //Otherwise, we can use this to call the service EditRestaurant method, and determine if it can be successfully edited.
            bool hasUpdated = await _service.UpdateRestaurant(model);
            //If something went wrong and the Restaurant has not been updated, again we could create an error page for this situation, but for now we can just return to the Edit view
            if(!hasUpdated) return View(model);
            return RedirectToAction(nameof(Details), new {id = model.Id});
        }

        public async Task<IActionResult> Delete (int id)
        {
            RestaurantDetail restaurant = await _service.GetRestaurantById(id);
            if(restaurant == null) 
                return RedirectToAction(nameof(Index));
            
            return View(restaurant);
        }

        [HttpPost]
        public async Task<IActionResult> Delete (int id, RestaurantDetail model)
        {
            bool wasDeleted = await _service.DeleteRestaurant(model.Id);
            if(wasDeleted)
                return RedirectToAction(nameof(Index));

            return View(model);
        }

        public async Task<IActionResult> DeleteRating (int id)
        {
            if(id == null)
            {
                return NotFound();
            }
        var rating = await _service.DeleteRestaurant(id);
        if(rating == null)
        {
            return NotFound();
        }
        return View(rating);
    }
}
}