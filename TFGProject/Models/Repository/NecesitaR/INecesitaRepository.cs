using TFGProject.Models.DTO;

namespace TFGProject.Models.Repository.NecesitaR
{
    public interface INecesitaRepository
    {
        Task<List<Necesita>> GetListNecesidadesPublicadas();
        Task<List<Necesita>> GetListNecesidadesBeneficiario(int id);
        Task<Necesita> GetNecesita(int id);
        Task DeleteNecesita(Necesita necesita);
        Task<Necesita> AddNecesita(Necesita necesita);
        Task UpdateNecesita(int id);
    }
}
