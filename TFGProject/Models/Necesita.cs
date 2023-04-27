


namespace TFGProject.Models
{
    public class Necesita
    {
        public int Estado { get; set; }
        public int Id { get; set; }
        public string Nombre { get; set; }
        public double Precio { get; set; }
        public int Cantidad { get; set; }
        public int IdBeneficiario { get; set; }
        public Beneficiario Beneficiario { get; set; }
    }
}
