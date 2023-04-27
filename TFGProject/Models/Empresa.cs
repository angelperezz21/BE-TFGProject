namespace TFGProject.Models
{
    public class Empresa : Usuario
    {
        public Empresa()
        {
            Recursos = new HashSet<Recurso>();
            Donaciones = new HashSet<Donacion>();
            BeneficiariosQueMeSiguen = new HashSet<BeneficiariosSiguenEmpresa>();
            BeneficiariosQueSigo = new HashSet<EmpresasSiguenBeneficiarios>();

        }
        public ICollection<Recurso> Recursos { get; set; }
        public ICollection<Donacion> Donaciones { get; set; }
        public ICollection<BeneficiariosSiguenEmpresa> BeneficiariosQueMeSiguen { get; set; }
        public ICollection<EmpresasSiguenBeneficiarios> BeneficiariosQueSigo { get; set; }
    }
}
