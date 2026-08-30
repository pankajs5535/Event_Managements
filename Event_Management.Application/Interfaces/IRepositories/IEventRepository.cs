
using Event_Management.Domain.Entities;
 

namespace Event_Management.Application.Interfaces.IRepositories
{
    public interface IEventRepository : IGenericRepository<Event>
    {
        Task<IEnumerable<Event>> SearchAsync(string searchTerm);

        Task<IEnumerable<Event>> GetByCategoryAsync(int categoryId);

        Task<IEnumerable<Event>> GetByStatusAsync(string status);

        Task<IEnumerable<Event>> GetPublicEventsAsync();

        Task<IEnumerable<Event>> GetUpcomingEventsAsync();

        Task<IEnumerable<Event>> GetByOrganizationAsync(long organizationId);
    }
}