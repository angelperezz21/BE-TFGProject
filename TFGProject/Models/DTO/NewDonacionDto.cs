namespace TFGProject.Models.DTO
{
    public class NewDonacionDto
    {
        public double valorTotal { get; set; }
        public DateTime FechaDonacion { get; set; }
        public string MetodoEntrega { get; set; }
        public int IdEmpresa { get; set; }
        public int IdBeneficiario { get; set; }

    }
}
