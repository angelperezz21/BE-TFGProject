

namespace TFGProject.Models.Estado
{
    public class PublicadoR : EstadoRecurso
    {
        public override void ControlarEstadoRecurso(Recurso rs)
        {
            rs.DefinirEstado(new SolicitadoR());
        }
    }
}
