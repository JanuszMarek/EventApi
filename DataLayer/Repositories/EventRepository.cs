using AutoMapper;
using DataLayer.Repositories.Abstract;
using Entities.Models;
using Infrastructure.Interfaces.IRepositories;

namespace DataLayer.Repositories
{
    public class EventRepository : 
        SoftDeleteRepository<Event, int>, 
        IEventRepository
    {
        public EventRepository(EventContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
