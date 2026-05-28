using DesignStudio.BLL.Interfaces;
using DesignStudio.BLL.Mapping;
using DesignStudio.BLL.Models;
using DesignStudio.DAL.Entities;
using DesignStudio.DAL.Interfaces;

namespace DesignStudio.BLL.Services
{
    public class ServiceCatalogService : IServiceCatalogService
    {
        private readonly IUnitOfWork _uow;

        public ServiceCatalogService(IUnitOfWork uow) => _uow = uow;

        public async Task<IEnumerable<ServiceModel>> GetAllServicesAsync()
        {
            var services = await _uow.Services.GetAllAsync();
            return services.Select(DalToBllMapper.ToModel);
        }

        public async Task AddServiceAsync(string name, string description,
            decimal price, string category)
        {
            if (price <= 0)
                throw new ArgumentException("Ціна повинна бути більше нуля.");

            var service = new Service
            {
                Name = name,
                Description = description,
                Price = price,
                Category = category
            };

            await _uow.Services.AddAsync(service);
            await _uow.SaveAsync();
        }
    }
}