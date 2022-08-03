using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace RestaurantRaterMVC.Data
{
    public class RestaurantEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        [MaxLength(100)]
        public string Location { get; set; }

        public virtual List<RatingEntity> Ratings { get; set; } = new List<RatingEntity>();
        //This is a virtual property, meaning this doesn't represent a column in the database, this is just a space we're leaving for EntityFramework 
        //to collect and deposit the associated Ratings for this Restaurant. EF does this using the Foreign Key relationship - each Rating has a RestaurantId, so 
        //EF just looks for all the Ratings with the right RestaurantId and fills this list with them for us.

        //Below this, we can add a property for the average value of each of the Rating scores for the Restaurant - AverageFoodScore, AverageCleanlinessScore, 
        //and AverageAtmosphereScore. We can use the LINQ Select() method to turn the List of Ratings into a List of Score values, then add them together using the Sum() method

        public double AverageFoodScore {
            get
            {
                //return Ratings.Select(r => r.FoodScore).Sum() / Ratings.Count;
                return Ratings.Count > 0 ? Ratings.Select(r => r.FoodScore).Sum() / Ratings.Count : 0;
            }
        }
        public double AverageCleanlinessScore
        {
            get
            {
                //return Ratings.Select(r => r.CleanlinessScore).Sum() / Ratings.Count;
                return Ratings.Count > 0 ? Ratings.Select(r => r.CleanlinessScore).Sum() / Ratings.Count : 0;
            }
        }
        public double AverageAtmosphereScore
        {
            get
            {
                
                return Ratings.Count > 0 ? Ratings.Select(r => r.AtmosphereScore).Sum() / Ratings.Count : 0;

            }
            //If a Restaurant has no Ratings, then we will get a divide by 0 error here, so let's add a bit of logic to fix this - if the number of Ratings is 
            //greater than 0, then we can average them, but if not, then let's just return a 0. Modify each getter like so:
        }
        public double Score
        {
            get
            {
                return (AverageFoodScore + AverageCleanlinessScore + AverageAtmosphereScore) / 3;
            }
        }
    }
}


