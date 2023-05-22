using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using test1.Dtos;
using test1.Models;
using Microsoft.EntityFrameworkCore;
using test1.Services;

namespace test1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMoviesService _moviesService;
        private readonly IGenraService _genraService;
        private List<string> _allowedExtentions = new List<string> { ".jpg", ".png" };
        private long _maxLength = 4194304;

        public MoviesController(IMoviesService moviesService, IGenraService genraService)
        {
            _moviesService = moviesService;
            _genraService = genraService;
        }

        [HttpGet("GetAllMovies")]
        public async Task<IActionResult> GetAllMoviesAsync()
        {
            var movies = await _moviesService.GetAllMoviesAsync(null);
            return Ok(movies);

        }

        [HttpGet("GetMovieById/{id}")]
        public async Task<IActionResult>GetMovieByIdAsync(int id)
        {
            var movie = await _moviesService.GetMovieByIdAsync(id);
            if (movie == null)
                return NotFound();

            return Ok(movie);
        }
        [HttpGet("GetMoviesByGenraId")]
        public async Task<IActionResult> GetMoviesByGenraIdAsync(int id)
        {
            var movie = await _moviesService.GetAllMoviesAsync(id);
            if (movie == null)
                return NotFound();

            return Ok(movie);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm]MoviesDto dto)
        {
            if (dto.Poster is null)
                return BadRequest("Poster is requird");
            if (!_allowedExtentions.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))
                return BadRequest("only .png and .jpg are allowed");
            if (_maxLength<dto.Poster.Length)
                return BadRequest("Max length is 4MB!");
            var isValidGenra =await _genraService.isValidGenra(dto.GenraId);
            if (!isValidGenra)
                return BadRequest("This Genra is not found");

            using var dataStreem = new MemoryStream();
            await dto.Poster.CopyToAsync(dataStreem);
            var movie = new Movie
            {
                Title = dto.Title,
                Year = dto.Year,
                Description = dto.Description,
                GenraId = dto.GenraId,
                Rate = dto.Rate,
                Poster = dataStreem.ToArray(),
            };
           await _moviesService.CreateMovieAsync(movie);

            return Ok(movie);
            
        }
        [HttpPut]
        public async Task<IActionResult> UpdateMovieAsync(int id , [FromForm]MoviesDto dto)
        {

            var movie = await _moviesService.GetMovieByIdAsync(id);
            if (movie == null) return NotFound($"No movie was found with id : {id}");
            var isValidGenra = await _genraService.isValidGenra(dto.GenraId);
            if (!isValidGenra)
                return BadRequest("This Genra is not found");

            if(dto.Poster is not null)
            {
                if (!_allowedExtentions.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))
                    return BadRequest("only .png and .jpg are allowed");
                if (_maxLength < dto.Poster.Length)
                    return BadRequest("Max length is 4MB!");

                using var dataStreem = new MemoryStream();
                await dto.Poster.CopyToAsync(dataStreem);
                movie.Poster= dataStreem.ToArray();
            }

            movie.Title = dto.Title;
            movie.Year = dto.Year;
            movie.Description = dto.Description;
            movie.Rate = dto.Rate;
            movie.GenraId = dto.GenraId;

            _moviesService.UpdateMovie(movie);

            return Ok(movie);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteMovieAsync(int id)
        {
            var movie = await _moviesService.GetMovieByIdAsync(id);
            if (movie == null) return NotFound($"this movie with id: {id} is not found");
            _moviesService.DeleteMovie(movie);
            return Ok(movie);
        }
    }
}
