using Microsoft.EntityFrameworkCore;
using test1.Models;

namespace test1.Services
{
    public class GenraService : IGenraService
    {
        private readonly ApplicationDbContext _context;

        public GenraService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Genra> CreateGenraAsync(Genra genra)
        {
            await _context.Genras.AddAsync(genra);
            _context.SaveChanges();
            return genra;
        }

        public Genra DeleteGenra(Genra genra)
        {
            _context.Remove(genra);
            _context.SaveChanges();
            return genra;
        }

        public async Task<IEnumerable<Genra>> GetAllGenraAsync()
        {
            return await _context.Genras.OrderBy(o => o.Name).ToListAsync();
        }

        public async Task<Genra> GetGenraByIdAsync(int id)
        {
            return await _context.Genras.FindAsync(id);
        }

        public async Task<bool> isValidGenra(int id)
        {
           return await _context.Genras.AnyAsync(g => g.Id == id);
        }

        public Genra UpdateGenra(Genra genra)
        {
            _context.Update(genra);
            _context.SaveChanges();
            return genra;
            
        }
    }
}
