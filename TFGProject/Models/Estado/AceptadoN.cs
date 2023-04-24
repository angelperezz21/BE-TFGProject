namespace TFGProject.Models.Estado
{
    public class AceptadoN : EstadoNecesidad
    {
        public override void ControlarEstadoNecesidad(Necesita necesita)
        {
            necesita.DefinirEstado(new BorradorN());
        }
    }
}
