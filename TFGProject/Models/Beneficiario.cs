namespace TFGProject.Models
{
    public class Beneficiario : Usuario
    {
        public Beneficiario()
        {
            Necesidades = new HashSet<Necesita>();
            Donaciones = new HashSet<Donacion>();
            EmpresasQueSigo = new HashSet<BeneficiariosSiguenEmpresa>();
            EmpresasQueMeSiguen = new HashSet<EmpresasSiguenBeneficiarios>();
        }
        public ICollection<Necesita> Necesidades { get; set; }
        public ICollection<Donacion> Donaciones { get; set; }
        public ICollection<BeneficiariosSiguenEmpresa> EmpresasQueSigo { get; set; }
        public ICollection<EmpresasSiguenBeneficiarios> EmpresasQueMeSiguen { get; set; }


    }
}
