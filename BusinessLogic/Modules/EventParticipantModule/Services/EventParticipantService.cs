using AutoMapper;
using Entities.Interfaces;
using Infrastructure.Interfaces.IRepositories;
using Infrastructure.Interfaces.IServices;
using System.Threading.Tasks;

namespace BusinessLogic.Modules.EventParticipantModule.Services
{
    public class EventParticipantService : IEventParticipantService
    {
        private readonly IEventParticipantRepository repository;

        public EventParticipantService(IEventParticipantRepository repository)
        {
            this.repository = repository;
        }

        public async Task<int?> GetExistingParticipantId(IEventParticipant eventParticipant)
        {
            return await repository.GetIdByAsync(
                x => x.LastName.ToLower() == eventParticipant.LastName.ToLower().Trim()
                && x.FirstName.ToLower() == eventParticipant.FirstName.ToLower().Trim()
                && x.Email.ToLower() == eventParticipant.Email.ToLower().Trim());
        }
    }
}
