using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace MovieApiTechTest.Controllers
{
    [ApiController]
    [Route("movies")]
    public class MoviesController : ControllerBase
    {
        private readonly MovieDatabase MovieDatabase;

        public MoviesController(MovieDatabase movieDatabase)
        {
            MovieDatabase = movieDatabase;
        }


        [Route("stats")]
        [Produces("application/json")]
        public IActionResult Stats()
        {
            return new OkObjectResult(MovieDatabase.GetMovieStats().OrderByDescending(x=>x.Watches).ThenByDescending(x=>x.ReleaseYear));
        }
    }
}
