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
            recurso.FechaCreacionRecurso = DateTime.Now;
            _context.Add(recurso);
            await _context.SaveChangesAsync();
            return recurso;
        }

        public async Task DeleteRecurso(Recurso recurso)
        {
            _context.Recursos.Remove(recurso);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Recurso>> GetListRecursosPublicados()
        {
            var empresas = await _context.Empresas.Include(x => x.Recursos).ToListAsync();
            List<Recurso> listRecursos = new List<Recurso>();
            foreach (var empresa in empresas)
            {
                foreach (var rec in empresa.Recursos)
                {
                    if (rec.Estado == 1 || rec.Estado == 2)
                    {
                        listRecursos.Add(rec);
                    }
                }
            }
                
            return listRecursos;
        }

        public async Task<List<Recurso>> GetListRecursosEmpresa(int id)
        {
            var empresa = await _context.Empresas.Include(x=>x.Recursos).FirstOrDefaultAsync(x => x.Id == id);

            return empresa.Recursos.ToList();
        }

        public async Task<Recurso> GetRecurso(int id)
        {
            return await _context.Recursos.FindAsync(id);
        }

        public async Task<Recurso> SolicitarRecurso(int idRecurso, int idBeneficiario)
        {
            var recurso =  await _context.Recursos.FindAsync(idRecurso);
            if (recurso.Estado == 1) recurso.Estado++;
            else return null;
            recurso.IdSolicitante = idBeneficiario;
            await _context.SaveChangesAsync();
            return recurso;
        }
        public async Task<Recurso> AceptarRecurso(int id)
        {
            var recurso = await _context.Recursos.FindAsync(id);
            if (recurso.Estado == 2) recurso.Estado++;
            else return null;
            await _context.SaveChangesAsync();
            return recurso;
        }
        public async Task<Recurso> PublicarRecurso(int id)
        {
            var recurso = await _context.Recursos.FindAsync(id);
            if (recurso.Estado == 3) recurso.Estado=1;
            else return null;
            await _context.SaveChangesAsync();
            return recurso;
        }
    }
}
