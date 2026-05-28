using DesignStudio.BLL.Interfaces;
using DesignStudio.BLL.Models;

namespace DesignStudio.UI
{
    public class ConsoleMenu
    {
        private readonly IOrderService _orderService;
        private readonly IPortfolioService _portfolioService;
        private readonly IServiceCatalogService _catalogService;

        public ConsoleMenu(IOrderService orderService,
                           IPortfolioService portfolioService,
                           IServiceCatalogService catalogService)
        {
            _orderService = orderService;
            _portfolioService = portfolioService;
            _catalogService = catalogService;
        }

        public async Task RunAsync()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Студія дизайну ===");
                Console.WriteLine("1. Переглянути каталог послуг");
                Console.WriteLine("2. Переглянути портфоліо");
                Console.WriteLine("3. Оформити замовлення (з переліку послуг)");
                Console.WriteLine("4. Оформити замовлення «під ключ»");
                Console.WriteLine("5. Переглянути замовлення");
                Console.WriteLine("6. Змінити статус замовлення");
                Console.WriteLine("7. Додати роботу до портфоліо");
                Console.WriteLine("8. Додати нову послугу");
                Console.WriteLine("0. Вихід");
                Console.Write("Ваш вибір: ");

                var choice = Console.ReadLine();
                try
                {
                    switch (choice)
                    {
                        case "1": await ShowServicesAsync(); break;
                        case "2": await ShowPortfolioAsync(); break;
                        case "3": await PlaceServiceOrderAsync(); break;
                        case "4": await PlaceTurnkeyOrderAsync(); break;
                        case "5": await ShowOrdersAsync(); break;
                        case "6": await UpdateOrderStatusAsync(); break;
                        case "7": await AddPortfolioItemAsync(); break;
                        case "8": await AddServiceAsync(); break;
                        case "0": return;
                        default: Console.WriteLine("Невідома команда."); break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\n[Помилка] {ex.Message}");
                }

                Console.WriteLine("\nНатисніть будь-яку клавішу...");
                Console.ReadKey();
            }
        }

        private async Task ShowServicesAsync()
        {
            var list = await _catalogService.GetAllServicesAsync();
            Console.WriteLine("\n--- Послуги ---");
            foreach (var s in list)
                Console.WriteLine($"[{s.Id}] {s.Name} | {s.Category} | {s.Price:C}");
        }

        private async Task ShowPortfolioAsync()
        {
            var list = await _portfolioService.GetAllAsync();
            Console.WriteLine("\n--- Портфоліо ---");
            foreach (var p in list)
                Console.WriteLine($"[{p.Id}] {p.Title} ({p.Category}) — {p.CompletedAt:yyyy-MM-dd}");
        }

        private async Task ShowOrdersAsync()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            Console.WriteLine("\n--- Замовлення ---");
            foreach (var o in orders)
            {
                Console.WriteLine($"[{o.Id}] {o.ClientName} | {o.Type} | {o.Status} | {o.CreatedAt:yyyy-MM-dd}");
                foreach (var s in o.Services)
                    Console.WriteLine($"     - {s.Name}");
            }
        }

        private async Task PlaceServiceOrderAsync()
        {
            await ShowServicesAsync();
            Console.Write("Ваше ім'я: "); var name = Console.ReadLine()!;
            Console.Write("Email: "); var email = Console.ReadLine()!;
            Console.Write("Вимоги/коментар: "); var req = Console.ReadLine()!;
            Console.Write("ID послуг (через кому): ");
            var ids = Console.ReadLine()!
                             .Split(',')
                             .Select(x => int.Parse(x.Trim()));

            var order = await _orderService.PlaceServiceOrderAsync(name, email, req, ids);
            Console.WriteLine($"\nЗамовлення #{order.Id} створено.");
        }

        private async Task PlaceTurnkeyOrderAsync()
        {
            Console.Write("Ваше ім'я: "); var name = Console.ReadLine()!;
            Console.Write("Email: "); var email = Console.ReadLine()!;
            Console.Write("Опис проєкту: "); var req = Console.ReadLine()!;

            var order = await _orderService.PlaceTurnkeyOrderAsync(name, email, req);
            Console.WriteLine($"\nЗамовлення «під ключ» #{order.Id} створено.");
        }

        private async Task UpdateOrderStatusAsync()
        {
            await ShowOrdersAsync();
            Console.Write("ID замовлення: "); var id = int.Parse(Console.ReadLine()!);
            Console.Write("Новий статус (0=Pending, 1=InProgress, 2=Completed, 3=Cancelled): ");
            var status = (OrderStatusModel)int.Parse(Console.ReadLine()!);

            await _orderService.UpdateOrderStatusAsync(id, status);
            Console.WriteLine("Статус оновлено.");
        }

        private async Task AddPortfolioItemAsync()
        {
            Console.Write("Назва: "); var title = Console.ReadLine()!;
            Console.Write("Опис: "); var desc = Console.ReadLine()!;
            Console.Write("Категорія: "); var cat = Console.ReadLine()!;
            Console.Write("URL зображення: "); var url = Console.ReadLine()!;
            Console.Write("ID замовлення (або пусто): ");
            var rawId = Console.ReadLine();
            int? orderId = string.IsNullOrWhiteSpace(rawId) ? null : int.Parse(rawId);

            await _portfolioService.AddPortfolioItemAsync(title, desc, cat, url, orderId);
            Console.WriteLine("Роботу додано до портфоліо.");
        }

        private async Task AddServiceAsync()
        {
            Console.Write("Назва: "); var name = Console.ReadLine()!;
            Console.Write("Опис: "); var desc = Console.ReadLine()!;
            Console.Write("Категорія: "); var cat = Console.ReadLine()!;
            Console.Write("Ціна (грн): "); var price = decimal.Parse(Console.ReadLine()!);

            await _catalogService.AddServiceAsync(name, desc, price, cat);
            Console.WriteLine("Послугу додано.");
        }
    }
}