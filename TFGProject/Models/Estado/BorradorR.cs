
namespace TFGProject.Models.Estado
{
    public class BorradorR : EstadoRecurso
    {
        public override void ControlarEstadoRecurso(Recurso rs)
        {
            rs.DefinirEstado(new PublicadoR());
        }
    }
}
