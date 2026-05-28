using DesignStudio.DAL.Entities;

namespace DesignStudio.DAL
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(DesignStudioContext context)
        {
            if (context.Services.Any()) return;

            var services = new[]
            {
                new Service { Name = "Розробка сайту",       Category = "Web",      Price = 15000, Description = "Повний цикл веб-дизайну" },
                new Service { Name = "Брендинг",             Category = "Branding", Price = 8000,  Description = "Логотип, фірмовий стиль" },
                new Service { Name = "Дизайн інтер'єру",     Category = "Interior", Price = 25000, Description = "Концепція та 3D-візуалізація" },
                new Service { Name = "UX/UI для застосунку", Category = "Web",      Price = 12000, Description = "Прототипування та дизайн" },
            };
            context.Services.AddRange(services);

            var portfolioItems = new[]
            {
                new PortfolioItem { Title = "Сайт для кав'ярні «Зерно»", Category = "Web",      CompletedAt = DateTime.UtcNow.AddMonths(-3), Description = "Мінімалістичний веб-сайт" },
                new PortfolioItem { Title = "Інтер'єр офісу TechCorp",   Category = "Interior", CompletedAt = DateTime.UtcNow.AddMonths(-1), Description = "Сучасний open-space" },
                new PortfolioItem { Title = "Фірмовий стиль «Maple»",    Category = "Branding", CompletedAt = DateTime.UtcNow.AddMonths(-2), Description = "Повний брендинг стартапу" },
            };
            context.PortfolioItems.AddRange(portfolioItems);

            await context.SaveChangesAsync();
        }
    }
}