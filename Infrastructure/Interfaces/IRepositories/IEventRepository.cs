using Entities.Models;

namespace Infrastructure.Interfaces.IRepositories
{
    public interface IEventRepository: IBaseRepository<Event, int>
    {
    }
}
