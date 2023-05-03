using TFGProject.Models.DTO;

namespace TFGProject.Models.Repository.RecursoR
{
    public interface IRecursoRepository
    {
        Task<List<Recurso>> GetListRecursosPublicados();
        Task<List<Recurso>> GetListRecursosEmpresa(int id);
        Task<Recurso> GetRecurso(int id);
        Task DeleteRecurso(Recurso recurso);
        Task<Recurso> AddRecurso(Recurso recurso);
        Task<Recurso> SolicitarRecurso(int idRecurso, int idBeneficiario);
        Task<Recurso> AceptarRecurso(int id);
        Task<Recurso> PublicarRecurso(int id);
    }
}
