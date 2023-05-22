using test1.Models;

namespace test1.Services
{
    public interface IMoviesService
    {
        Task<IEnumerable<Movie>> GetAllMoviesAsync(int? id);
        Task<Movie> GetMovieByIdAsync(int id);
        Task<Movie> CreateMovieAsync(Movie movie);
        Movie DeleteMovie(Movie movie);
        Movie UpdateMovie(Movie movie);
    }
}
