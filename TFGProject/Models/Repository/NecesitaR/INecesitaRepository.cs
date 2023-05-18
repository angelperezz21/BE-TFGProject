using TFGProject.Models.DTO;

namespace TFGProject.Models.Repository.NecesitaR
{
    public interface INecesitaRepository
    {
        Task<List<Necesita>> GetListNecesidadesPublicadas();
        Task<List<Necesita>> GetListNecesidadesBeneficiario(int id);
        Task<Necesita> GetNecesita(int id);
        Task DeleteNecesita(Necesita necesita);
        Task UpdateNecesita(NecesitaDto necesitaDto, int id);
        Task<Necesita> AddNecesita(Necesita necesita);
        Task<Necesita> SolicitarNecesidad(int idNecesita, int idEmpresa);
        Task<Necesita> AceptarNecesidad(int idNecesita, int idEmpresa);
        Task<List<SolicitanteDto>> GetNotificaciones(NecesitaDto necesita);
        Task<List<Necesita>> GetSolicitudesNecesita(int id);
    }
}
