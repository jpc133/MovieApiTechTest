using Microsoft.AspNetCore.Mvc;
using MovieApiTechTest.Model;
using System.Collections.Generic;
using System.Linq;

namespace MovieApiTechTest.Controllers
{
    [ApiController]
    [Route("metadata")]
    public class MetaDataController : Controller
    {
        private readonly MovieDatabase MovieDatabase;

        public MetaDataController(MovieDatabase movieDatabase)
        {
            MovieDatabase = movieDatabase;
        }

        [HttpPost]
        public IActionResult Index([FromBody]Movie movie)
        {
            MovieDatabase.AddMetaData(movie);
            return new OkResult();
        }

        [HttpGet]
        [Route("{movieId}")]
        public IActionResult Index(int movieId)
        {
            List<Movie> movies = MovieDatabase.GetLatestMetaData(movieId);
            if (movies == null || movies.Count == 0)
            {
                return NotFound();
            }
            return new OkObjectResult(movies.OrderBy(x => x.Language));
        }
    }
}
