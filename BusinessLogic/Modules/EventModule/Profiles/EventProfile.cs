using AutoMapper;
using BusinessModels.Modules.EventModule.Models;
using Entities.Models;

namespace BusinessLogic.Modules.EventModule.Profiles
{
    public class EventProfile : Profile
    {
        public EventProfile()
        {
            MapModelsToEntities();
        }

        private void MapModelsToEntities()
        {
            CreateMap<EventCreateModel, Event>()
                .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dst => dst.TicketPool, opt => opt.MapFrom(src => src.TicketPool))
                .ForMember(dst => dst.IsDeleted, opt => opt.MapFrom(src => false))
                .ForMember(dst => dst.Id, opt => opt.Ignore())
                .ForMember(dst => dst.EventTickets, opt => opt.Ignore());

            CreateMap<EventCreateModel, Event>()
                .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dst => dst.TicketPool, opt => opt.MapFrom(src => src.TicketPool))
                .ForMember(dst => dst.IsDeleted, opt => opt.Ignore())
                .ForMember(dst => dst.Id, opt => opt.Ignore())
                .ForMember(dst => dst.EventTickets, opt => opt.Ignore());
        }
    }
}
