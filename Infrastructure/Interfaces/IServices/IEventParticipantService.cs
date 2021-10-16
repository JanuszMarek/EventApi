using Entities.Interfaces;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces.IServices
{
    public interface IEventParticipantService
    {
        Task<int?> GetExistingParticipantId(IEventParticipant eventParticipant);
    }
}