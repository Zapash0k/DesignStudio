using DesignStudio.BLL.Interfaces;
using DesignStudio.BLL.Services;
using DesignStudio.DAL;
using DesignStudio.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DesignStudio.BLL.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDesignStudioServices(
            this IServiceCollection services,
            string connectionString)
        {
            // DAL — реєструється тут, PL про це не знає
            services.AddDbContext<DesignStudioContext>(opt =>
                opt.UseSqlite(connectionString));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // BLL
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IPortfolioService, PortfolioService>();
            services.AddScoped<IServiceCatalogService, ServiceCatalogService>();

            return services;
        }
    }
}