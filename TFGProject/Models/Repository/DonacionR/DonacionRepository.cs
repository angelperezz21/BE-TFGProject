using Microsoft.EntityFrameworkCore;

namespace TFGProject.Models.Repository.DonacionR
{
    public class DonacionRepository : IDonacionRepository
    {
        private readonly ApplicationDbContext _context;

        public DonacionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Donacion> AddDonacion(Donacion donacion, bool cert)
        {
            if (cert)
            {
                var certificado = new Certificado { Ruta = "a" };
                _context.Add(certificado);
                donacion.Certificado = certificado;
            }
            _context.Add(donacion);
            await _context.SaveChangesAsync();
            return donacion;
        }

        public async Task<List<Donacion>> GetListDonacions()
        {
            return await _context.Donaciones.ToListAsync();
        }

        public async Task<Donacion> GetDonacion(int id)
        {
            return await _context.Donaciones.FirstOrDefaultAsync(x=>x.Id==id);
        }
        public async Task<Certificado> GetCertificadoDonacion(int id)
        {
            return await _context.Certificados.FirstOrDefaultAsync(x => x.Id == id);
        }

    }
}
