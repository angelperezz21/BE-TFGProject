namespace TFGProject.Models.Estado
{
    public class BorradorN : EstadoNecesidad
    {
        public override void ControlarEstadoNecesidad(Necesita necesita)
        {
            necesita.DefinirEstado(new PublicadoN());
        }
    }
}
