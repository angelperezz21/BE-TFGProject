namespace TFGProject.Models
{
    public class Recurso
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public double Precio { get; set; }
        public int Cantidad { get; set; }
        public string MetodoEntrega { get; set; }
        public int IdEmpresa { get; set; }
        public Empresa Empresa { get; set; }

    }
}
