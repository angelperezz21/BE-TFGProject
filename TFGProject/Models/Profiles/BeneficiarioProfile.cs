using AutoMapper;
using TFGProject.Models.DTO;

namespace TFGProject.Models.Profiles
{
   public class BeneficiarioProfile : Profile
    {
        public BeneficiarioProfile()
        {
            CreateMap<Beneficiario, BeneficiarioDto>();
            CreateMap<BeneficiarioDto, Beneficiario>();

            CreateMap<Beneficiario, BeneficiarioPerfilDto>();
            CreateMap<BeneficiarioPerfilDto, Beneficiario>();
        }
    }
}
