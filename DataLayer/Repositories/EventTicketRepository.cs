using AutoMapper;
using Entities.Models;

namespace DataLayer.Repositories
{
    public class EventTicketRepository : BaseRepository<EventTicket, long>
    {
        public EventTicketRepository(EventContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
