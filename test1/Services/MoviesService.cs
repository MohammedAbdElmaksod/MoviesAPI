using Microsoft.EntityFrameworkCore;
using test1.Models;

namespace test1.Services
{
    public class MoviesService : IMoviesService
    {
        private readonly ApplicationDbContext _context;

        public MoviesService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Movie> CreateMovieAsync(Movie movie)
        {
            await _context.AddAsync(movie);
            _context.SaveChanges();
            return movie;
        }

        public Movie DeleteMovie(Movie movie)
        {
            _context.Remove(movie);
            _context.SaveChanges();
            return movie;
        }

        public async Task<IEnumerable<Movie>> GetAllMoviesAsync(int? id)
        {
            if (id == null)
            {
                return await _context.Movies.Include(m => m.genra)
               .OrderByDescending(m => m.Rate)
               .ToListAsync();
            }
            return await _context.Movies.Include(m => m.genra)
                .Where(m => m.GenraId==id)
               .OrderByDescending(m => m.Rate)
               .ToListAsync();
        }

        public async Task<Movie> GetMovieByIdAsync(int id)
        {
            var movie = await _context.Movies.Include(m => m.genra).SingleOrDefaultAsync(m => m.Id == id);
            return movie;
        }

        public Movie UpdateMovie(Movie movie)
        {
            _context.Movies.Update(movie);
            _context.SaveChanges();
            return movie;
        }
    }
}
