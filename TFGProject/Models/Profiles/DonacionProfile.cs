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



            CreateMap<Donacion, NewDonacionDto>();
            CreateMap<NewDonacionDto, Donacion>();
        }
    }
}
