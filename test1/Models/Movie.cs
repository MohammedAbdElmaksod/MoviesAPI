using System.ComponentModel.DataAnnotations;

namespace test1.Models
{
    public class Movie
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Title { get; set; }
        [MaxLength(2500)]
        public string Description { get; set; }
        public int Year { get; set; }
        public double Rate { get; set; }
        public int GenraId { get; set; }
        public byte[] Poster { get; set; }
        public Genra genra { get; set; }

    }
}
