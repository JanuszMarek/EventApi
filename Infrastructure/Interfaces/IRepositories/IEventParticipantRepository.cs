using Entities.Models;
using Infrastructure.Interfaces.IRepositories.Abstract;

namespace Infrastructure.Interfaces.IRepositories
{
    public interface IEventParticipantRepository : IBaseRepository<EventParticipant, int>
    {
    }
}
