namespace TFGProject.Models.DTO
{
    public class RecursoDto
    {
        public int Estado { get; set; }
        public int Id { get; set; }
        public string Nombre { get; set; }
        public double Precio { get; set; }
        public int Cantidad { get; set; }
        public string MetodoEntrega { get; set; }
        public int IdEmpresa { get; set; }
        public string? imgUrl { get; set; }
        public string Descripcion { get; set; }
        public DateTime? FechaCreacionRecurso { get; set; }        
        public string? Solicitantes { get; set; }

    }
}
