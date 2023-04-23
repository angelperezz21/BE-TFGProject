using Microsoft.EntityFrameworkCore;
using TFGProject.Models.DTO;

namespace TFGProject.Models.Repository.RecursoR
{
    public class RecursoRepository : IRecursoRepository
    {
        private readonly ApplicationDbContext _context;

        public RecursoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Recurso> AddRecurso(Recurso recurso)
        {
            _context.Add(recurso);
            await _context.SaveChangesAsync();
            return recurso;
        }

        public async Task DeleteRecurso(Recurso recurso)
        {
            _context.Recursos.Remove(recurso);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Recurso>> GetListRecursos()
        {
            return await _context.Recursos.ToListAsync();
        }

        public async Task<Recurso> GetRecurso(int id)
        {
            return await _context.Recursos.FindAsync(id);
        }
    }
}
