using AutoMapper;
using Infrastructure.Interfaces.IRepositories;

namespace BusinessLogic.Modules.EventModule.Services
{
    public class EventService
    {
        private readonly IEventRepository repository;
        private readonly IMapper mapper;

        public EventService(IEventRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
    }
}
