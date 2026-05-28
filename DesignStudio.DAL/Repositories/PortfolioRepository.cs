using DesignStudio.DAL.Entities;
using DesignStudio.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DesignStudio.DAL.Repositories
{
    public class PortfolioRepository : Repository<PortfolioItem>, IPortfolioRepository
    {
        public PortfolioRepository(DesignStudioContext context) : base(context) { }

        public async Task<IEnumerable<PortfolioItem>> GetByCategoryAsync(string category)
            => await _context.PortfolioItems
                .Where(p => p.Category == category)
                .ToListAsync();
    }
}