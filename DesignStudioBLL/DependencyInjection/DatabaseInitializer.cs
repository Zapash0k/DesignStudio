using DesignStudio.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DesignStudio.BLL.DependencyInjection
{
    public static class DatabaseInitializer
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();

            var context = scope.ServiceProvider
                               .GetRequiredService<DesignStudioContext>();

            await context.Database.MigrateAsync();
            await DbSeeder.SeedAsync(context);
        }
    }
}
