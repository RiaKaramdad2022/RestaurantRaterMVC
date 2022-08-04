using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantRaterMVC.Models
{
    public class RatingListItem
    {
        public int Id { get; set; }
        [Display(Name = "Restaurant")]
        public string RestaurantName { get; set; }
        [Display(Name = "Food Score")]
        public double FoodScore { get; set; }
        [Display(Name = "Cleanliness Score")]
        public double CleanlinessScore { get; set; }
        [Display(Name = "Atmosphere Score")]
        public double AtmosphereScore { get; set; }
    }
}