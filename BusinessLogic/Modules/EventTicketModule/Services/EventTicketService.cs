using AutoMapper;
using Infrastructure.Interfaces.IRepositories;

namespace BusinessLogic.Modules.EventTicketModule.Services
{
    public class EventTicketService
    {
        private readonly IEventTicketRepository repository;
        private readonly IMapper mapper;

        public EventTicketService(IEventTicketRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
    }
}
