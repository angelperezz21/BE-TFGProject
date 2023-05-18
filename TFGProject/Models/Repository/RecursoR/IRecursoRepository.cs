using TFGProject.Models.DTO;

namespace TFGProject.Models.Repository.RecursoR
{
    public interface IRecursoRepository
    {
        Task<List<Recurso>> GetListRecursosPublicados();
        Task<List<Recurso>> GetListRecursosEmpresa(int id);
        Task<Recurso> GetRecurso(int id);
        Task DeleteRecurso(Recurso recurso);
        Task UpdateRecurso(RecursoDto recursoDto, int id);
        Task<Recurso> AddRecurso(Recurso recurso);
        Task<Recurso> SolicitarRecurso(int idRecurso, int idBeneficiario);
        Task<Recurso> AceptarRecurso(int idRecurso, int idBeneficiario);
        Task<List<SolicitanteDto>> GetNotificaciones(RecursoDto recurso);
        Task<List<Recurso>> GetSolicitudesRecursos(int id);

    }
}
