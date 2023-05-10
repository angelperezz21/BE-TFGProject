namespace TFGProject.Models.Repository.DonacionR
{
    public interface IDonacionRepository
    {
        Task<List<Donacion>> GetListDonacions();
        Task<Donacion> GetDonacion(int id);
        Task<Donacion> AddDonacion(Donacion beneficiario);
        Task<string> GenerarPDFCertificado(Donacion donacion, string ruta);
    }
}
