namespace TFGProject.Models.Estado
{
    public class PublicadoN : EstadoNecesidad
    {
        public override void ControlarEstadoNecesidad(Necesita necesita)
        {
            necesita.DefinirEstado(new SolicitadoN());
        }
    }
}
