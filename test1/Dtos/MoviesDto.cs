using System.ComponentModel.DataAnnotations;

namespace test1.Dtos
{
    public class MoviesDto
    {
        [MaxLength(100)]
        public string Title { get; set; }
       
        [MaxLength(2500)]
        public string Description { get; set; }
        public int Year { get; set; }
        public double Rate { get; set; }
        public int GenraId { get; set; }
        public IFormFile? Poster { get; set; }
    }
}
