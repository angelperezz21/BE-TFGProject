using Microsoft.EntityFrameworkCore;
using TFGProject.Models.DTO;

namespace TFGProject.Models.Repository.EmpresaR
{
    public class EmpresaRepository : IEmpresaRepository
    {
        private readonly ApplicationDbContext _context;

        public EmpresaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Empresa> AddEmpresa(Empresa empresa)
        {
            var existEmpresa = _context.Empresas.FirstOrDefault(b => b.Email == empresa.Email);
            if (existEmpresa != null) return null;
            var existBeneficiario = _context.Beneficiarios.FirstOrDefault(b => b.Email == empresa.Email);
            if (existBeneficiario != null) return null;
            _context.Add(empresa);
            await _context.SaveChangesAsync();
            return empresa;
        }
        public async Task NuevoSeguido(int idBeneficiario,int idEmpresa)
        {
            var empresa = await _context.Empresas.FindAsync(idEmpresa);
            var beneficiario = await _context.Beneficiarios.FindAsync(idBeneficiario);

            empresa.BeneficiariosQueSigo.Add(
                new EmpresasSiguenBeneficiarios { Beneficiario = beneficiario, Empresa = empresa});

            await _context.SaveChangesAsync();
        }

        public async Task DeleteEmpresa(Empresa empresa)
        {
            _context.Empresas.Remove(empresa);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Empresa>> GetListEmpresas()
        {
            return await _context.Empresas.ToListAsync();
        }

        public async Task<List<Beneficiario>> GetBeneficiariosSeguidos(int id)
        {
            var empresa = await _context.Empresas.Include(x => x.BeneficiariosQueSigo).FirstOrDefaultAsync(x => x.Id == id);
            var beneficiarios = new List<Beneficiario>();
            foreach (var beneSigo in empresa.BeneficiariosQueSigo) {
                beneficiarios.Add(await _context.Beneficiarios.FindAsync(beneSigo.IdBeneficiario));
            }
            return beneficiarios;
        }

        public async Task<List<Donacion>> GetListDonaciones(int id)
        {
            var empresa = await _context.Empresas.Include(x=>x.Donaciones).FirstOrDefaultAsync(x=>x.Id==id);
            return empresa.Donaciones.ToList();
        }

        public async Task<Empresa> GetEmpresa(int id)
        {
            return await _context.Empresas.FindAsync(id);
        }

        public async Task UpdateEmpresa(EmpresaDto empresa, int id)
        {
            var empresaItem = await _context.Empresas.FirstOrDefaultAsync(x => x.Id == id);

            if (empresaItem != null)
            {
                if (empresa.Contrasenya != empresaItem.Email) empresaItem.Email = empresa.Contrasenya;
                if (empresa.Descripcion != empresaItem.Descripcion) empresaItem.Descripcion = empresa.Descripcion;
                if (empresa.Nombre != empresaItem.Nombre) empresaItem.Nombre = empresa.Nombre;
                if (empresa.Telefono != empresaItem.Telefono) empresaItem.Telefono = (int) empresa.Telefono;
                if (empresa.Direccion != empresaItem.Direccion) empresaItem.Direccion = empresa.Direccion;
                if (empresa.Web != empresaItem.Web) empresaItem.Web = empresa.Web;
                if (empresa.Contacto != empresaItem.Contacto) empresaItem.Contacto = empresa.Contacto;
                if (empresa.Categoria != empresaItem.Categoria) empresaItem.Categoria = empresa.Categoria;

                await _context.SaveChangesAsync();
            }

        }
    }
}
