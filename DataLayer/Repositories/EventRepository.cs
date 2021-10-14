using AutoMapper;
using Entities.Models;
using Infrastructure.Interfaces.IRepositories;

namespace DataLayer.Repositories
{
    public class EventRepository : 
        BaseRepository<Event, int>, 
        IEventRepository
    {
        public EventRepository(EventContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
