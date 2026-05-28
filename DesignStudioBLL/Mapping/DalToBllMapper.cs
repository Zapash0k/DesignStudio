using DesignStudio.BLL.Models;
using DesignStudio.DAL.Entities;

namespace DesignStudio.BLL.Mapping
{
    internal static class DalToBllMapper
    {
        public static ServiceModel ToModel(Service s) => new()
        {
            Id = s.Id,
            Name = s.Name,
            Description = s.Description,
            Price = s.Price,
            Category = s.Category
        };

        public static Service ToEntity(ServiceModel m) => new()
        {
            Id = m.Id,
            Name = m.Name,
            Description = m.Description,
            Price = m.Price,
            Category = m.Category
        };

        public static OrderModel ToModel(Order o) => new()
        {
            Id = o.Id,
            ClientName = o.ClientName,
            ClientEmail = o.ClientEmail,
            Requirements = o.Requirements,
            Status = (OrderStatusModel)o.Status,
            Type = (OrderTypeModel)o.Type,
            CreatedAt = o.CreatedAt,
            Services = o.OrderServices?
                            .Select(os => ToModel(os.Service))
                            .ToList() ?? new()
        };

        public static PortfolioItemModel ToModel(PortfolioItem p) => new()
        {
            Id = p.Id,
            Title = p.Title,
            Description = p.Description,
            Category = p.Category,
            CompletedAt = p.CompletedAt,
            OrderId = p.OrderId
        };
    }
}