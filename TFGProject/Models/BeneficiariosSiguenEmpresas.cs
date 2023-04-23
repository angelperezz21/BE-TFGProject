namespace TFGProject.Models
{
    public class BeneficiariosSiguenEmpresa
    {
        public int IdBeneficiario { get; set; }
        public int IdEmpresa { get; set; }
        public Beneficiario Beneficiario { get; set; }
        public Empresa Empresa { get; set; }

    }
}
