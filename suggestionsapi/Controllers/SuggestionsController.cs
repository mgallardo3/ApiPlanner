using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace PlannerApi.Controllers
{
    [ApiController]
    //[Route("[controller]")]
    [Route("api")]
    public class SuggestionsController : ControllerBase
    {
        /// <summary>
        ///All the weather conditions that will help the api decide what suggestions to make
        /// </summary>
        private string[] AllIds = new[]
        {
            "Clear","Clouds","Drizzle","Snow","Thurderstorm","Tornado","Rain"
        };
        /// <summary>
        /// All the suggestions for places to go visit
        /// </summary>
        private static readonly string[][] AllKeywords = new string[][]
        {
            new string[]{"beaches","waterfalls","climbing","pools","market_places","bycicle_rental"},
            new string[]{"ferris_wheels","parks","waterfalls","climbing","market_places","tourist_object"},
            new string[]{"museums","cafes", "theatres_and_entertainments", "museums_of_science_and_technology"},
            new string[]{"skiing","other_winter_sports","hot_springs","cafes","museums"},
            new string[]{"cafes","bars","cinemas","museums"},
            new string[]{"bars","cafes","cinemas","museums"},
            new string[]{"museums_of_science_and_technology", "theatres_and_entertainments","cinemas","cafes"}
        };

        /// <summary>
        /// An explanation about why the api suggested this place
        /// </summary>
        private static readonly string[] AllRecommendations = new[]
        {
            "Nice weather sky looks clear, here are some nice suggestions for you to do!",
            "Is cloudy but nice and warm out there, I found some places you might enjoy!",
            "Its a little drizzle out there, perhaps you would like to try some of the following activities:",
            "Wow is snowing!!, maybe some of these would be a nice thing to do today!",
            "Gosh there is a thurderstorm out there, you sure you want to go out? here are some suggestions!",
            "There is a tornato near by, stay safe!",
            "Rainy day, not too bad weather, here are some suggestions for you to do!"
        };

        private static readonly string[][] ColdKeywords = new string[][]
{
            new string[]{"market_places","towers","museums","gardens_and_parks"},
            new string[]{"museums","market_places","gardens_and_parks","cinemas"},
            new string[]{ "cafes","cinemas","theatres_and_entertainments","museums"}
};
        private static readonly string[] ColdRecommendations = new[]
{
            "Sky looks clear but kind off cold out there, I found some good places for you to hangout!",
            "Is cloudy and cold out there, I found some places you might enjoy!",
            "Its cold and drizzle out there, perhaps you would like to do some of these:"
        };

        private readonly ILogger<SuggestionsController> _logger;

        public SuggestionsController(ILogger<SuggestionsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Suggestions> Get()
        {
            var rng = new Random();
            return Enumerable.Range(0, AllIds.Count()).Select(index => new Suggestions
            {
                Id = AllIds[index],
                Keywords = AllKeywords[index],
                Recommendation = AllRecommendations[index]
            })
            .ToArray();
        }

        // GET: api/id/weather
        [HttpGet("id={id}&weather={weather}")]
        public Suggestions Get(string id, int weather)
        {
            //if valid inputs, proceed with the request
            if (AllIds.Contains(id))
            {
                int Index = Array.IndexOf(AllIds, id);
                //Special suggestions for cold weather
                if (weather< 68 &(id == "Clear" || id == "Clouds" || id == "Drizzle"))
                {
                    return new Suggestions
                    {
                        Id = AllIds[Index],
                        Keywords = ColdKeywords[Index],
                        Recommendation = ColdRecommendations[Index]
                    };
                }
                else
                {
                    return new Suggestions
                    {
                        Id = AllIds[Index],
                        Keywords = AllKeywords[Index],
                        Recommendation = AllRecommendations[Index]
                    };
                }
            }
            else
            {
                //return a null result for invalid inputs
                return new Suggestions { };
            }
        }

    }
}
