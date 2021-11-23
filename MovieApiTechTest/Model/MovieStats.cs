namespace MovieApiTechTest.Model
{
    public class MovieStats
    {
        public MovieStats(int movieId, string title, int averageWatchDurationS, int watches, int releaseYear)
        {
            MovieId = movieId;
            Title = title;
            AverageWatchDurationS = averageWatchDurationS;
            Watches = watches;
            ReleaseYear = releaseYear;
        }

        public int MovieId { get; set; }
        public string Title { get; set; }
        public int AverageWatchDurationS { get; set; }
        public int Watches { get; set; }
        public int ReleaseYear { get; set; }
    }
}
