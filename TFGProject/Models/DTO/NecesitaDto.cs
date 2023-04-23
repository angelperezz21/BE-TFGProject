namespace TFGProject.Models.DTO
{
    public class NecesitaDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public double Precio { get; set; }
        public int Cantidad { get; set; }
        public int IdBeneficiario { get; set; }
    }
}
