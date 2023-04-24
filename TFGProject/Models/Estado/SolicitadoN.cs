namespace TFGProject.Models.Estado
{
    public class SolicitadoN : EstadoNecesidad
    {
        public override void ControlarEstadoNecesidad(Necesita necesita)
        {
            necesita.DefinirEstado(new AceptadoN());
        }
    }
}
