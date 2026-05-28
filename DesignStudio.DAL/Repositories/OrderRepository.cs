using DesignStudio.DAL.Entities;
using DesignStudio.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DesignStudio.DAL.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(DesignStudioContext context) : base(context) { }

        public async Task<IEnumerable<Order>> GetAllWithDetailsAsync()
            => await _context.Orders
                .Include(o => o.OrderServices)
                    .ThenInclude(os => os.Service)
                .Include(o => o.PortfolioItem)
                .ToListAsync();

        public async Task<Order?> GetByIdWithDetailsAsync(int id)
            => await _context.Orders
                .Include(o => o.OrderServices)
                    .ThenInclude(os => os.Service)
                .Include(o => o.PortfolioItem)
                .FirstOrDefaultAsync(o => o.Id == id);
    }
}