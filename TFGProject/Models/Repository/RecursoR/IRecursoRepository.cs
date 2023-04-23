using TFGProject.Models.DTO;

namespace TFGProject.Models.Repository.RecursoR
{
    public interface IRecursoRepository
    {
        Task<List<Recurso>> GetListRecursos();
        Task<Recurso> GetRecurso(int id);
        Task DeleteRecurso(Recurso recurso);
        Task<Recurso> AddRecurso(Recurso recurso);
    }
}
