using test1.Models;

namespace test1.Services
{
    public interface IGenraService
    {
        Task<IEnumerable<Genra>> GetAllGenraAsync();
        Task<Genra> GetGenraByIdAsync(int id);
        Task<Genra> CreateGenraAsync(Genra genra);
        Genra UpdateGenra(Genra genra);
        Genra DeleteGenra(Genra genra);
        Task<bool> isValidGenra(int id);
    }
}
