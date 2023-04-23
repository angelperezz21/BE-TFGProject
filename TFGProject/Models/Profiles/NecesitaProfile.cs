using AutoMapper;
using TFGProject.Models.DTO;

namespace TFGProject.Models.Profiles
{
   public class NecesitaProfile : Profile
    {
        public NecesitaProfile()
        {
            CreateMap<Necesita, NecesitaDto>();
            CreateMap<NecesitaDto, Necesita>();
        }
    }
}
