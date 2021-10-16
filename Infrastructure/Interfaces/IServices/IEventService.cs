using BusinessModels.Modules.EventModule.DTOs;
using BusinessModels.Modules.EventModule.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces.IServices
{
    public interface IEventService
    {
        Task CreateAsync(EventCreateModel createModel);
        Task DeleteAsync(int eventId);
        Task EditAsync(int eventId, EventEditModel editModel);
        Task<EventDetailModel> GetDetailAsync(int eventId);
        Task<IEnumerable<EventDetailModel>> GetListAsync();
        Task<EventRemainingTicketDto> GetTicketRemainingCountAsync(int eventId);
    }
}