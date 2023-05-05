


using System.ComponentModel.DataAnnotations.Schema;

namespace TFGProject.Models
{
    public class Necesita
    {
        public int Estado { get; set; }
        public int Id { get; set; }
        public string Nombre { get; set; }
        public double Precio { get; set; }
        public int Cantidad { get; set; }
        public DateTime FechaCreacionRecurso { get; set; }
        public string Descripcion { get; set; }
        public bool Certificado { get; set; }
        public int IdBeneficiario { get; set; }
        public Beneficiario Beneficiario { get; set; }

        [Column(TypeName = "text")]
        public string? Solicitantes { get; set; }
    }
}
