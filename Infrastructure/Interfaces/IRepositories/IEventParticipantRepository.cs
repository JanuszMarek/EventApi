using Entities.Models;
using Infrastructure.Interfaces.IRepositories.Abstract;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces.IRepositories
{
    public interface IEventParticipantRepository : IBaseRepository<EventParticipant, int>
    {
        Task<int> GetIdByAsync(Expression<Func<EventParticipant, bool>> wherePredicte);
    }
}
