using Entities.Models;
using Infrastructure.Interfaces.IRepositories.Abstract;

namespace Infrastructure.Interfaces.IRepositories
{
    public interface IEventTicketRepository: IBaseRepository<EventTicket, long>
    {
    }
}
