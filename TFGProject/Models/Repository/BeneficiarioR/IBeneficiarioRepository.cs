﻿using TFGProject.Models.DTO;

namespace TFGProject.Models.Repository.BeneficiarioR
{
    public interface IBeneficiarioRepository
    {
        Task<List<Beneficiario>> GetListBeneficiarios();
        Task<Beneficiario> GetBeneficiario(int id);
        Task DeleteBeneficiario(Beneficiario beneficiario);
        Task<List<Donacion>> GetListDonaciones(int id);
        Task<Beneficiario> AddBeneficiario(Beneficiario beneficiario);
        Task UpdateBeneficiario(BeneficiarioDto beneficiario, int id);
        Task<List<Empresa>> GetEmpresasSeguidos(int id);
        Task NuevoSeguido(int idBeneficiario, int idEmpresa);
    }
}
