using AutoMapper;
using TFGProject.Models.DTO;

namespace TFGProject.Models.Profiles
{
   public class DonacionProfile : Profile
    {
        public DonacionProfile()
        {
            CreateMap<Donacion, DonacionDto>();
            CreateMap<DonacionDto, Donacion>();

            CreateMap<Certificado, CertificadoDto>();
            CreateMap<CertificadoDto, Certificado>();

            CreateMap<Donacion, NewDonacionDto>();
            CreateMap<NewDonacionDto, Donacion>();
        }
    }
}
