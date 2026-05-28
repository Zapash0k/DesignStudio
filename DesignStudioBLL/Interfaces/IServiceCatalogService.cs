using DesignStudio.BLL.Models;

namespace DesignStudio.BLL.Interfaces
{
    public interface IServiceCatalogService
    {
        Task<IEnumerable<ServiceModel>> GetAllServicesAsync();
        Task AddServiceAsync(string name, string description, decimal price, string category);
    }
}