using AutoMapper;
using Entities.Models;

namespace DataLayer.Repositories
{
    public class EventRepository : BaseRepository<Event, int>
    {
        public EventRepository(EventContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
