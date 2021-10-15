using AutoMapper;
using BusinessModels.Modules.EventModule.Models;
using Entities.Models;
using System.Linq;

namespace BusinessLogic.Modules.EventModule.Profiles
{
    public class EventProfile : Profile
    {
        public EventProfile()
        {
            MapModelsToEntities();
            MapEntitiesToModels();
        }

        private void MapModelsToEntities()
        {
            CreateMap<EventCreateModel, Event>()
                .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dst => dst.TicketPool, opt => opt.MapFrom(src => src.TicketPool))
                .ForMember(dst => dst.IsDeleted, opt => opt.MapFrom(src => false))
                .ForMember(dst => dst.Id, opt => opt.Ignore())
                .ForMember(dst => dst.EventTickets, opt => opt.Ignore());

            CreateMap<EventEditModel, Event>()
                .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dst => dst.TicketPool, opt => opt.MapFrom(src => src.TicketPool))
                .ForMember(dst => dst.IsDeleted, opt => opt.Ignore())
                .ForMember(dst => dst.Id, opt => opt.Ignore())
                .ForMember(dst => dst.EventTickets, opt => opt.Ignore());
        }

        private void MapEntitiesToModels()
        {
            CreateMap<Event, EventDetailModel>()
                .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dst => dst.TicketsRemaining, opt => opt.MapFrom(src => src.TicketPool - src.EventTickets.Count));
        }
    }
}
