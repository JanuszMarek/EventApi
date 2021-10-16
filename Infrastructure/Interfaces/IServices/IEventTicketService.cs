using BusinessModels.Modules.EventTicketModule.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces.IServices
{
    public interface IEventTicketService
    {
        Task<bool> BuyTicketAsync(int eventId, EventTicketCreateModel createModel);
        Task<IEnumerable<EventTicketListModel>> GetListForEventAsync(int eventId);
        Task ReturnTicketAsync(long ticketId);
    }
}