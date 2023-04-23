namespace TFGProject.Models.Repository.DonacionR
{
    public interface IDonacionRepository
    {
        Task<List<Donacion>> GetListDonacions();
        Task<Donacion> GetDonacion(int id);
        Task<Donacion> AddDonacion(Donacion beneficiario, bool cert);
        Task<Certificado> GetCertificadoDonacion(int id);
    }
}
