using TFGProject.Models.DTO;

namespace TFGProject.Models.Repository.NecesitaR
{
    public interface INecesitaRepository
    {
        Task<List<Necesita>> GetListNecesitas();
        Task<Necesita> GetNecesita(int id);
        Task DeleteNecesita(Necesita necesita);
        Task<Necesita> AddNecesita(Necesita necesita);
    }
}
