using AutoMapper;
using TFGProject.Models.DTO;

namespace TFGProject.Models.Profiles
{
   public class EmpresaProfile : Profile
    {
        public EmpresaProfile()
        {
            CreateMap<Empresa, EmpresaDto>();
            CreateMap<EmpresaDto, Empresa>();

            CreateMap<Empresa, EmpresaPerfilDto>();
            CreateMap<EmpresaPerfilDto, Empresa>();
        }
    }
}
