using AutoMapper;
using TFGProject.Models.DTO;

namespace TFGProject.Models.Profiles
{
   public class RecursoProfile : Profile
    {
        public RecursoProfile()
        {
            CreateMap<Recurso, RecursoDto>();
            CreateMap<RecursoDto, Recurso>();
        }
    }
}
