namespace TFGProject.Models.DTO
{
    public class NecesitaDto
    {
        public int Estado { get; set; }
        public int Id { get; set; }
        public string Nombre { get; set; }
        public double Precio { get; set; }
        public int Cantidad { get; set; }
        public string Descripcion { get; set; }
        public DateTime? FechaCreacionNecesita { get; set; }
        public int IdBeneficiario { get; set; }
        public bool Certificado { get; set; }
        public string? Solicitantes { get; set; }
    }
}
