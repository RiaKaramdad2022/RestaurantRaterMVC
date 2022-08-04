using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantRaterMVC.Models
{
    public class RatingCreate
    {
        [Required]
        public int RestaurantId { get; set; }
        [Required]
        [Range(0, 5)]
        public double FoodScore {get; set; }
        [Required]
        [Range(0, 5)]
        public double AtmosphereScore { get; set; }
        [Required]
        [Range(0, 5)]
        public double CleanlinessScore { get; set;}
    }
}