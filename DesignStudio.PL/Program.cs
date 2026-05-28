using DesignStudio.BLL.DependencyInjection;
using DesignStudio.BLL.Interfaces;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();

// PL знає лише про BLL — один виклик
services.AddDesignStudioServices("Data Source=designstudio.db");

var provider = services.BuildServiceProvider();

// Ініціалізація через BLL-метод, без прямого EF у PL
await DatabaseInitializer.InitializeAsync(provider);

using var scope = provider.CreateScope();
var menu = new DesignStudio.UI.ConsoleMenu(
    scope.ServiceProvider.GetRequiredService<IOrderService>(),
    scope.ServiceProvider.GetRequiredService<IPortfolioService>(),
    scope.ServiceProvider.GetRequiredService<IServiceCatalogService>()
);
await menu.RunAsync();