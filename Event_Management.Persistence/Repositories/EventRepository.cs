using Event_Management.Application.Interfaces.IRepositories;
using Event_Management.Domain.Entities;
using Event_Management.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Event_Management.Persistence.Repositories
{
    public class EventRepository : GenericRepository<Event>, IEventRepository
    {
        public EventRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Event>> SearchAsync(string searchTerm)
        {
            return await _dbSet.Where(e => e.Title.Contains(searchTerm) || e.EventCode.Contains(searchTerm) || 
            (e.Description != null && e.Description.Contains(searchTerm))).ToListAsync();
        }

        public async Task<IEnumerable<Event>> GetByCategoryAsync(int categoryId)
        {
            return await _dbSet.Where(e => e.CategoryId == categoryId).ToListAsync();
        }

        public async Task<IEnumerable<Event>> GetByStatusAsync(string status)
        {
            return await _dbSet.Where(e => e.Status == status).ToListAsync();
        }

        public async Task<IEnumerable<Event>> GetPublicEventsAsync()
        {
            return await _dbSet.Where(e => e.IsPublic).ToListAsync();
        }

        public async Task<IEnumerable<Event>> GetUpcomingEventsAsync()
        {
            return await _dbSet.Where(e => e.StartDate >= DateTime.UtcNow).OrderBy(e => e.StartDate).ToListAsync();
        }

        public async Task<IEnumerable<Event>> GetByOrganizationAsync(long organizationId)
        {
            return await _dbSet.Where(e => e.OrganizationId == organizationId).ToListAsync();
        }
    }
}