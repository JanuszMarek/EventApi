using AutoMapper;
using BusinessModels.Modules.EventModule.Models;
using Entities.Models;
using Infrastructure.Interfaces.IRepositories;
using Infrastructure.Interfaces.IServices;
using System.Collections.Generic;
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

        public async Task<IEnumerable<EventDetailModel>> GetListAsync()
        {
            return await repository.GetRangeAsync<EventDetailModel>();
        }

        public async Task<EventDetailModel> GetDetailAsync(int eventId)
        {
            return await repository.GetAsync<EventDetailModel>(eventId);
        }

        public async Task CreateAsync(EventCreateModel createModel)
        {
            var newEvent = mapper.Map<Event>(createModel);

            await repository.AddAsync(newEvent);
            await repository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int eventId)
        {
            var eventToDelete = await repository.GetAsync(eventId);

            if (eventToDelete != null)
            {
                repository.Delete(eventToDelete);
                await repository.SaveChangesAsync();
            }
        }

        public async Task EditAsync(int eventId, EventEditModel editModel)
        {
            var eventToEdit = await repository.GetAsync(eventId);

            if (eventToEdit != null)
            {
                mapper.Map(editModel, eventToEdit);
                await repository.SaveChangesAsync();
            }
        }
    }
}
