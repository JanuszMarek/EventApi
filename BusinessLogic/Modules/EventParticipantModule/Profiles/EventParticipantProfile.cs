using AutoMapper;
using BusinessModels.Modules.EventParticipantModule.DTOs;
using Entities.Models;
using System;

namespace BusinessLogic.Modules.EventParticipantModule.Profiles
{
    public class EventParticipantProfile : Profile
    {
        public EventParticipantProfile()
        {
            MapModelsToEntities();
            MapEntitiesToModels();
        }


        private void MapModelsToEntities()
        {
            CreateMap<EventParticipantCreateModel, EventParticipant>()
                .ForMember(dst => dst.FirstName, opt => opt.MapFrom(src => src.FirstName.Trim()))
                .ForMember(dst => dst.LastName, opt => opt.MapFrom(src => src.LastName.Trim()))
                .ForMember(dst => dst.Email, opt => opt.MapFrom(src => src.Email.Trim()))
                .ForMember(dst => dst.Id, opt => opt.Ignore());
        }

        private void MapEntitiesToModels()
        {
            CreateMap<EventParticipant, EventParticipantModel>()
                .ForMember(dst => dst.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dst => dst.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dst => dst.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id));
        }
    }
}
