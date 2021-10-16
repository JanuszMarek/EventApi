using AutoMapper;
using BusinessModels.Modules.EventTicketModule.Models;
using Entities.Models;
using System;

namespace BusinessLogic.Modules.EventTicketModule.Profiles
{
    public class EventTicketProfile : Profile
    {
        public EventTicketProfile()
        {
            MapEntitiesToModels();
        }

        private void MapEntitiesToModels()
        {
            CreateMap<EventTicket, EventTicketListModel>()
                .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.EventParticipant, opt => opt.MapFrom(src => src.EventParticipant));
        }
    }
}
