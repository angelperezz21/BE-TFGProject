namespace TFGProject.Models.DTO
{
    public class DonacionDto
    {
        public int Id { get; set; }
        public double valorTotal { get; set; }
        public DateTime FechaDonacion { get; set; }
        public string NombreRecurso { get; set; }
        public int Cantidad { get; set; }
        public string MetodoEntrega { get; set; }
        public int IdEmpresa { get; set; }
        public int IdBeneficiario { get; set; }
    }
}
