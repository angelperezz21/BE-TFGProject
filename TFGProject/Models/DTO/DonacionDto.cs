namespace TFGProject.Models.DTO
{
    public class DonacionDto
    {
        public int Id { get; set; }
        public double valorTotal { get; set; }
        public DateTime FechaDonacion { get; set; }
        public string NombreRecurso { get; set; }
        public int Cantidad { get; set; }
        public bool Enviada { get; set; }
        public bool Recibida { get; set; }
        public string MetodoEntrega { get; set; }
        public int IdEmpresa { get; set; }
        public int IdBeneficiario { get; set; }
        public EmpresaPerfilDto Empresa { get; set; }
        public BeneficiarioPerfilDto Beneficiario { get; set; }
    }
}
