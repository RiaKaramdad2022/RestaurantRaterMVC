using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantRaterMVC.Data
{
    public class RatingEntity
    {
        //We will need an Id and a RestaurantId (both ints), and our three scores (all doubles). The Id is the Key and the RestaurantId is a Foreign Key, 
        //pointing to the Restaurant model, but Entity Framework will assume this even without the [Key] and [ForeignKey] annotations due to our naming convention:
        public int Id { get; set; }
        public int RestaurantId { get; set; }
        public double FoodScore { get; set; }
        public double CleanlinessScore { get; set; }
        public double AtmosphereScore { get; set; }

        //We can also add a virtual Restaurant, so the associated Restaurant data will be available when we query Ratings.
        public virtual RestaurantEntity Restaurant { get; set; }
    }
}