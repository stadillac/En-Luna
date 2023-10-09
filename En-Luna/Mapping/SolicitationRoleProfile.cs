using AutoMapper;
using En_Luna.Data.Models;
using En_Luna.ViewModels;

namespace Jobbie.Web.Mapping
{
    public class SolicitationRoleProfile : Profile
    {
        public SolicitationRoleProfile()
        {
            CreateMap<SolicitationRole, SolicitationRoleViewModel>().ReverseMap();
            CreateMap<SolicitationRole, SolicitationRoleEditViewModel>().ReverseMap();
        }
    }
}
