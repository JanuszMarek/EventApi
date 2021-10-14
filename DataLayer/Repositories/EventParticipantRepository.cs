using AutoMapper;
using Entities.Models;
using Infrastructure.Interfaces.IRepositories;

namespace DataLayer.Repositories
{
    public class EventParticipantRepository : 
        BaseRepository<EventParticipant, int>, 
        IEventParticipantRepository
    {
        public EventParticipantRepository(EventContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
