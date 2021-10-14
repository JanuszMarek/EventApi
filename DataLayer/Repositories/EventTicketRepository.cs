using AutoMapper;
using Entities.Models;
using Infrastructure.Interfaces.IRepositories;

namespace DataLayer.Repositories
{
    public class EventTicketRepository : 
        BaseRepository<EventTicket, long>,
        IEventTicketRepository
    {
        public EventTicketRepository(EventContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
