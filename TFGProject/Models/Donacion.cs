namespace TFGProject.Models
{
    public class Donacion
    {
        public int Id { get; set; }
        public double valorTotal { get; set; }
        public string NombreRecurso { get; set; }
        public int Cantidad { get; set; }
        public DateTime FechaDonacion { get; set; }
        public string MetodoEntrega { get; set; }
        public int IdEmpresa { get; set; }
        public int IdBeneficiario { get; set; }
        public Empresa Empresa { get; set; }
        public Beneficiario Beneficiario { get; set; }
        public int? IdCertificado { get; set; }
        public Certificado? Certificado { get; set; }
    }
}
