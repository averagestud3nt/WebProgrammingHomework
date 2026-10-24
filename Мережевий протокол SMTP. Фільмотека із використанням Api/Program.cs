namespace HomeWorkWinForms
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm());
        }

        //static async Task Main()
        //{
        //    var allGenres = await MovieApi.GetGenreList();
        //    var allMovies = await MovieApi.GetPopularMovies(page: 1);
        //    var allMoviesByName = await MovieApi.GetMoviesByName("Star wars", page: 1);
        //    var allMoviesByGenre = await MovieApi.GetMoviesByGenre(allGenres.genres[0].id, page: 1);
        //    var allMoviesByNameAndYear = MovieApi.GetMoviesByYearAndName("Star Wars", 2001);
        //    var currentMovie = await MovieApi.GetMovieById(allMoviesByGenre.Results[0].id);
        //    var similarMovies = await MovieApi.GetSimilarMovies(allMoviesByGenre.Results[0].id, page: 1);
        //}
    }
}