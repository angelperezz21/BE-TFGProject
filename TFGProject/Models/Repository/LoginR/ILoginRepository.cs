using TFGProject.Models.DTO;

namespace TFGProject.Models.Repository.LoginR
{
    public interface ILoginRepository
    {
        Task<EmpresaLogin> LoginEmpresa(LoginUsuario loginUsuario);
        Task<BeneficiarioLogin> LoginBeneficiario(LoginUsuario loginUsuario);
        
    }
}
