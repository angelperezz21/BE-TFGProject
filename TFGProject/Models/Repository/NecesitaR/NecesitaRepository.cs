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

        public async Task<List<Necesita>> GetListNecesidadesBeneficiario(int id)
        {

            var beneficiario = await _context.Beneficiarios.Include(x => x.Necesidades).FirstOrDefaultAsync(x => x.Id == id);

            return beneficiario.Necesidades.ToList();
        }

        public async Task<List<Necesita>> GetListNecesidadesPublicadas()
        {
            var beneficiarios = await _context.Beneficiarios.Include(x => x.Necesidades).ToListAsync();
            List<Necesita> listNecesidades = new List<Necesita>();
            foreach (var beneficiario in beneficiarios)
            {
                foreach (var ben in beneficiario.Necesidades)
                {
                    if (ben.Estado == 0 || ben.Estado == 1)
                    {
                        listNecesidades.Add(ben);
                    }
                }
            }

            return listNecesidades;
        }

        public async Task<Necesita> GetNecesita(int id)
        {
            return await _context.Necesidades.FindAsync(id);
        }

        public async Task<Necesita> SolicitarNecesidad(int idNecesidad, int idEmpresa)
        {
            var necesita = await _context.Necesidades.FindAsync(idNecesidad);
            if (necesita.Estado == 1) necesita.Estado++;
            else return null;
            necesita.IdSolicitante = idEmpresa;
            await _context.SaveChangesAsync();
            return necesita;
        }
        public async Task<Necesita> AceptarNecesidad(int id)
        {
            var necesita = await _context.Necesidades.FindAsync(id);
            if (necesita.Estado == 2) necesita.Estado++;
            else return null;
            await _context.SaveChangesAsync();
            return necesita;
        }
        public async Task<Necesita> PublicarNecesidad(int id)
        {
            var necesita = await _context.Necesidades.FindAsync(id);
            if (necesita.Estado == 3) necesita.Estado = 1;
            else return null;
            await _context.SaveChangesAsync();
            return necesita;
        }
    }
}
