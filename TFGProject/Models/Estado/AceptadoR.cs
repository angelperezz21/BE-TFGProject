

namespace TFGProject.Models.Estado
{
    public class AceptadoR : EstadoRecurso
    {
        public override void ControlarEstadoRecurso(Recurso rs)
        {
            rs.DefinirEstado(new AceptadoR());
        }
    }
}
