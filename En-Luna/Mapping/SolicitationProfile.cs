using AutoMapper;
using En_Luna.Data.Models;
using En_Luna.ViewModels;

namespace Jobbie.Web.Mapping
{
    public class SolicitationProfile : Profile
    {
        public SolicitationProfile()
        {
            CreateMap<Solicitation, SolicitationViewModel>().ReverseMap();
            CreateMap<SolicitationEditViewModel, Solicitation>()
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(
                    x => x.SolicitationRoles
                ));
            CreateMap<Solicitation, SolicitationEditViewModel>()
                .ForMember(dest => dest.States, opt => opt.Ignore());
        }
    }
}
