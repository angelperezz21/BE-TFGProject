

using System.ComponentModel.DataAnnotations.Schema;

namespace TFGProject.Models
{
    public class Recurso
    {
        public int Estado { get; set; }
        public int Id { get; set; }
        public string Nombre { get; set; }
        public double Precio { get; set; }
        public int Cantidad { get; set; }
        public string MetodoEntrega { get; set; }
        public DateTime FechaCreacionRecurso { get; set; }
        public bool Certificado { get; set; }
        public int IdEmpresa { get; set; }
        public Empresa Empresa { get; set; }
        [Column(TypeName = "text")]
        public string? Solicitantes { get; set; }
    }
}
