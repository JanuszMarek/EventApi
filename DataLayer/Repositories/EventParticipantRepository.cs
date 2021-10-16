using AutoMapper;
using DataLayer.Repositories.Abstract;
using Entities.Models;
using Infrastructure.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    public class EventParticipantRepository : 
        BaseRepository<EventParticipant, int>, 
        IEventParticipantRepository
    {
        public EventParticipantRepository(EventContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<int> GetIdByAsync(Expression<Func<EventParticipant, bool>> wherePredicte)
        {
            return await GetWhereIncludeQuery(wherePredicte, null)
                .Select(x => x.Id)
                .FirstOrDefaultAsync();
        }
    }
}
