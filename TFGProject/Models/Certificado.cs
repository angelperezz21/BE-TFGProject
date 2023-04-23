namespace TFGProject.Models
{
    public class Certificado
    {
        public int Id { get; set; }
        public string Ruta { get; set; }
        public Donacion Donacion { get; set; }
    }
}
