namespace TFGProject.Models.DTO
{
    public class NewDonacionDto
    {
        public int Id { get; set; }
        public string NombreRecurso { get; set; }
        public bool Enviada { get; set; }
        public bool Recibida { get; set; }
        public int IdEmpresa { get; set; }
        public int IdBeneficiario { get; set; }
        public EmpresaPerfilDto Empresa { get; set; }
        public BeneficiarioPerfilDto Beneficiario { get; set; }
    }
}
