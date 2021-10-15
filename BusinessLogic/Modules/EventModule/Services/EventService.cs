using AutoMapper;
using BusinessModels.Modules.EventModule.Models;
using Entities.Models;
using Infrastructure.Interfaces.IRepositories;
using Infrastructure.Interfaces.IServices;
using System.Threading.Tasks;

namespace BusinessLogic.Modules.EventModule.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository repository;
        private readonly IMapper mapper;

        public EventService(IEventRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<EventDetailModel> GetDetailsAsync(int eventId)
        {
            return await repository.GetActiveAsync<EventDetailModel>(eventId);
        }

        public async Task CreateAsync(EventCreateModel createModel)
        {
            var newEvent = mapper.Map<Event>(createModel);

            await repository.AddAsync(newEvent);
            await repository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int eventId)
        {
            var eventToDelete = await repository.GetActiveAsync(eventId);

            if (eventToDelete != null)
            {
                repository.Delete(eventToDelete);
                await repository.SaveChangesAsync();
            }
        }

        public async Task EditAsync(int eventId, EventEditModel editModel)
        {
            var eventToEdit = await repository.GetActiveAsync(eventId);

            if (eventToEdit != null)
            {
                mapper.Map(editModel, eventToEdit);
                await repository.SaveChangesAsync();
            }
        }
    }
}
