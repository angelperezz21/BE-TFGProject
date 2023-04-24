
using TFGProject.Models.Estado;

namespace TFGProject.Models
{
    public class Necesita
    {
        public Necesita()
        {
            _estado = new BorradorN();
        }

        public void DefinirEstado(EstadoNecesidad estado)
        {
            _estado = estado;
        }

        public EstadoNecesidad _estado;
        public int Id { get; set; }
        public string Nombre { get; set; }
        public double Precio { get; set; }
        public int Cantidad { get; set; }
        public int IdBeneficiario { get; set; }
        public Beneficiario Beneficiario { get; set; }
    }
}
