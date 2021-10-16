using AutoMapper;
using BusinessModels.Modules.EventTicketModule.Models;
using Entities.Models;
using Infrastructure.Interfaces.IRepositories;
using Infrastructure.Interfaces.IServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic.Modules.EventTicketModule.Services
{
    public class EventTicketService : IEventTicketService
    {
        private readonly IEventTicketRepository repository;
        private readonly IMapper mapper;
        private readonly IEventService eventService;
        private readonly IEventParticipantService eventParticipantService;

        public EventTicketService(
            IEventTicketRepository repository,
            IMapper mapper,
            IEventService eventService,
            IEventParticipantService eventParticipantService)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.eventService = eventService;
            this.eventParticipantService = eventParticipantService;
        }

        public async Task<bool> BuyTicketAsync(int eventId, EventTicketCreateModel createModel)
        {
            var response = false;

            var remainingTicketDto = await eventService.GetTicketRemainingCountAsync(eventId);
            if (remainingTicketDto.Count > 0)
            {
                var newTicket = new EventTicket()
                {
                    EventId = eventId,
                    IsDeleted = false
                };

                await SetTicketParticipant(createModel, newTicket);

                await repository.AddAsync(newTicket);
                await repository.SaveChangesAsync();

                response = true;
            }

            return response;

            async Task SetTicketParticipant(EventTicketCreateModel createModel, EventTicket newTicket)
            {
                var participant = createModel.EventParticipant;
                var existingParticipantId = await eventParticipantService.GetExistingParticipantId(participant);

                if (existingParticipantId.HasValue && existingParticipantId.Value != default)
                {
                    newTicket.EventParticipantId = existingParticipantId.Value;
                }
                else
                {
                    newTicket.EventParticipant = mapper.Map<EventParticipant>(participant);
                }
            }
        }

        public async Task ReturnTicketAsync(long ticketId)
        {
            var ticketToDelete = await repository.GetAsync(ticketId);

            repository.Delete(ticketToDelete);
            await repository.SaveChangesAsync();
        }

        public async Task<IEnumerable<EventTicketListModel>> GetListForEventAsync(int eventId)
        {
            return await repository.GetRangeAsync<EventTicketListModel>(x => x.EventId == eventId);
        }
    }
}
