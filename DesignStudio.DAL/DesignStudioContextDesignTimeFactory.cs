using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DesignStudio.DAL
{
    // Потрібна тільки для команд dotnet ef — не використовується в runtime
    public class DesignStudioContextDesignTimeFactory
        : IDesignTimeDbContextFactory<DesignStudioContext>
    {
        public DesignStudioContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DesignStudioContext>();
            optionsBuilder.UseSqlite("Data Source=designstudio.db");
            return new DesignStudioContext(optionsBuilder.Options);
        }
    }
}
