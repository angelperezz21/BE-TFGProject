using TFGProject.Models.DTO;

namespace TFGProject.Models.Repository.EmpresaR
{
    public interface IEmpresaRepository
    {
        Task<List<Empresa>> GetListEmpresas();
        Task<Empresa> GetEmpresa(int id);
        Task<List<Donacion>> GetListDonaciones(int id);
        Task DeleteEmpresa(Empresa empresa);
        Task<Empresa> AddEmpresa(Empresa empresa);
        Task UpdateEmpresa(EmpresaDto empresa, int id);
        Task<List<Beneficiario>> GetBeneficiariosSeguidos(int id);
        Task NuevoSeguido(int idBeneficiario, int idEmpresa);
        Task GetContrasenya(string email);
    }
}
