using Entities.Models;
using Infrastructure.Interfaces.IRepositories.Abstract;

namespace Infrastructure.Interfaces.IRepositories
{
    public interface IEventRepository: IBaseRepository<Event, int>
    {
    }
}
