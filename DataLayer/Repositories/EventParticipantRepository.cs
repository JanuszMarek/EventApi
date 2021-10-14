using AutoMapper;
using Entities.Models;

namespace DataLayer.Repositories
{
    public class EventParticipantRepository : BaseRepository<EventParticipant, int>
    {
        public EventParticipantRepository(EventContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
