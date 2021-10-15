using BusinessModels.Modules.EventModule.Models;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces.IServices
{
    public interface IEventService
    {
        Task CreateAsync(EventCreateModel createModel);
        Task DeleteAsync(int eventId);
        Task EditAsync(int eventId, EventEditModel editModel);
        Task<EventDetailModel> GetDetailsAsync(int eventId);
    }
}