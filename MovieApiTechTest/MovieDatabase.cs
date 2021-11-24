using CsvHelper;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using MovieApiTechTest.Model;
using System.Linq;
using CsvHelper.Configuration;

namespace MovieApiTechTest
{
    public class MovieDatabase
    {
        private const string DefaultStatsLanguage = "EN";

        private readonly List<Movie> Movies;
        private readonly List<MovieWatch> MovieWatches;

        public MovieDatabase()
        {
            CsvConfiguration config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                PrepareHeaderForMatch = args => args.Header.ToLower(), //Stats file uses headers with lower case first letter - so we can use case insensitive matching to preserve naming schemes
            };

            using (var reader = new StreamReader("TaskFiles/metadata.csv"))
            using (var csv = new CsvReader(reader, config))
            {
                Movies = csv.GetRecords<Movie>().ToList();
            }

            using (var reader = new StreamReader("TaskFiles/stats.csv"))
            using (var csv = new CsvReader(reader, config))
            {
                MovieWatches = csv.GetRecords<MovieWatch>().ToList();
            }
        }

        public void AddMetaData(Movie movie)
        {
            Movies.Add(movie);
            movie.Id = Movies.Max(x => x.Id) + 1;
        }

        /// <summary>
        /// Returns the most recent complete metadata for each language for the given movie
        /// </summary>
        /// <param name="movieId"></param>
        /// <returns></returns>
        public List<Movie> GetLatestMetaData(int movieId)
        {
            List<Movie> requestedMovie = Movies.Where(x => x.MovieId == movieId && x.IsComplete()).OrderByDescending(x => x.Id).ToList(); //Select just the right movie where complete and order most recent data first
            return requestedMovie.GroupBy(x => x.Language).Select(x => x.First()).ToList(); //Group metadata by language then select the first (most recent given ordering) metadata
        }

        public List<MovieStats> GetMovieStats()
        {
            List<Movie> latestMetadataForStatsLanguage = Movies.Where(x => x.Language == DefaultStatsLanguage).GroupBy(x => x.MovieId).Select(x => x.OrderByDescending(y => y.Id).First()).ToList();

            List<MovieStats> movieStats = new List<MovieStats>();
            foreach (var movie in latestMetadataForStatsLanguage)
            {
                List<MovieWatch> movieWatches = MovieWatches.Where(x => x.MovieId == movie.MovieId).ToList();
                movieStats.Add(new MovieStats(movie.MovieId, movie.Title, movieWatches.Count == 0 ? 0 : (int)movieWatches.Average(x => x.WatchDurationMs), movieWatches.Count, movie.ReleaseYear ?? 0));
            }

            return movieStats;
        }
    }
}
