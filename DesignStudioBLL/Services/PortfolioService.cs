using DesignStudio.BLL.Interfaces;
using DesignStudio.BLL.Mapping;
using DesignStudio.BLL.Models;
using DesignStudio.DAL.Entities;
using DesignStudio.DAL.Interfaces;

namespace DesignStudio.BLL.Services
{
    public class PortfolioService : IPortfolioService
    {
        private readonly IUnitOfWork _uow;

        public PortfolioService(IUnitOfWork uow) => _uow = uow;

        public async Task<IEnumerable<PortfolioItemModel>> GetAllAsync()
        {
            var items = await _uow.Portfolio.GetAllAsync();
            return items.Select(DalToBllMapper.ToModel);
        }

        public async Task<IEnumerable<PortfolioItemModel>> GetByCategoryAsync(string category)
        {
            var items = await _uow.Portfolio.GetByCategoryAsync(category);
            return items.Select(DalToBllMapper.ToModel);
        }

        public async Task AddPortfolioItemAsync(string title, string description,
            string category, string imageUrl, int? orderId)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Назва обов'язкова.");

            var item = new PortfolioItem
            {
                Title = title,
                Description = description,
                Category = category,
                OrderId = orderId,
                CompletedAt = DateTime.UtcNow
            };

            await _uow.Portfolio.AddAsync(item);
            await _uow.SaveAsync();
        }
    }
}