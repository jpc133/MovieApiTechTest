namespace MovieApiTechTest.Model
{
    public class Movie
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public string Title { get; set; }
        public string Language { get; set; }
        public string Duration { get; set; }
        public int? ReleaseYear { get; set; }

        /// <summary>
        /// Returns true if all fields are filled
        /// </summary>
        /// <returns></returns>
        public bool IsComplete()
        {
            return (Title != null) && (Language != null) && (Duration != null) && (ReleaseYear != null);
        }
    }
}
