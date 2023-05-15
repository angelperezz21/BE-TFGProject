namespace TFGProject.Models.Repository.DonacionR
{
    public interface IDonacionRepository
    {
        Task<List<Donacion>> GetListDonacions();
        Task<Donacion> GetDonacion(int id);
        Task<Donacion> UpdateEnvioDonacion(int id);
        Task<Donacion> UpdateRecibirDonacion(int id);
        Task<Donacion> AddDonacion(Donacion beneficiario);
        Task<string> GenerarPDFCertificado(Donacion donacion, string ruta);
    }
}
