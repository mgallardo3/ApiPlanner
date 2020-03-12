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
            new string[]{"cinemas,indoor,parks"},
            new string[]{"cinemas,indoor,parks"},
            new string[]{"cinemas,indoor,parks"},
            new string[]{"cinemas,indoor,parks"},
            new string[]{"cinemas,indoor,parks"},
            new string[]{"cinemas,indoor,parks"},
            new string[]{"cinemas,indoor,parks"}
        };

        /// <summary>
        /// An explanation about why the api suggested this place
        /// </summary>
        private static readonly string[] AllRecommendations = new[]
        {
            "Nice weather sky looks clear, here are some nice suggestions for you to do!",
            "Is cloudy but nice and warm out there, I found some places you might enjoy!",
            "This are all the recommendations",
            "This are all the recommendations",
            "This are all the recommendations",
            "This are all the recommendations",
            "This are all the recommendations"
        };

        private static readonly string[][] ColdKeywords = new string[][]
{
            new string[]{"cinemas,indoor,parks"},
            new string[]{"cinemas,indoor,parks"},
            new string[]{"cinemas,indoor,parks"},
            new string[]{"cinemas,indoor,parks"},
            new string[]{"cinemas,indoor,parks"},
            new string[]{"cinemas,indoor,parks"},
            new string[]{"cinemas,indoor,parks"}
};
        private static readonly string[] ColdRecommendations = new[]
{
            "Sky looks clear but kind off cold out there, I found some good places for you to hangout!",
            "Is cloudy and cold out there, I found some places you might enjoy!",
            "This are all the recommendations",
            "This are all the recommendations",
            "This are all the recommendations",
            "This are all the recommendations",
            "This are all the recommendations"
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

        // GET: api/id/
        [HttpGet("id={id}&weather={weather}")]
        public Suggestions Get(string id, int weather)
        {
            if (AllIds.Contains(id))
            {
                int Index = Array.IndexOf(AllIds, id);
                if (weather > 68)
                {
                    return new Suggestions
                    {
                        Id = AllIds[Index],
                        Keywords = AllKeywords[Index],
                        Recommendation = AllRecommendations[Index]
                    };
                }
                else
                {
                    return new Suggestions
                    {
                        Id = AllIds[Index],
                        Keywords = ColdKeywords[Index],
                        Recommendation = ColdRecommendations[Index]
                    };
                }
            }
            else
            {
                return new Suggestions { };
            }
        }

    }
}
