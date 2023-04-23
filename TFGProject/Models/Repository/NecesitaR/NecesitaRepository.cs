using Microsoft.EntityFrameworkCore;
using TFGProject.Models.DTO;

namespace TFGProject.Models.Repository.NecesitaR
{
    public class NecesitaRepository : INecesitaRepository
    {
        private readonly ApplicationDbContext _context;

        public NecesitaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Necesita> AddNecesita(Necesita necesita)
        {
            _context.Add(necesita);
            await _context.SaveChangesAsync();
            return necesita;
        }

        public async Task DeleteNecesita(Necesita necesita)
        {
            _context.Necesidades.Remove(necesita);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Necesita>> GetListNecesitas()
        {
            return await _context.Necesidades.ToListAsync();
        }

        public async Task<Necesita> GetNecesita(int id)
        {
            return await _context.Necesidades.FindAsync(id);
        }
    }
}
