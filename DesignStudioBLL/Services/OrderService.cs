using DesignStudio.BLL.Interfaces;
using DesignStudio.BLL.Mapping;
using DesignStudio.BLL.Models;
using DesignStudio.DAL.Entities;
using DesignStudio.DAL.Interfaces;
using DALEntities = DesignStudio.DAL.Entities;

namespace DesignStudio.BLL.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _uow;

        public OrderService(IUnitOfWork uow) => _uow = uow;

        public async Task<IEnumerable<OrderModel>> GetAllOrdersAsync()
        {
            var orders = await _uow.Orders.GetAllWithDetailsAsync();
            return orders.Select(DalToBllMapper.ToModel);
        }

        public async Task<OrderModel?> GetOrderByIdAsync(int id)
        {
            var order = await _uow.Orders.GetByIdWithDetailsAsync(id);
            return order is null ? null : DalToBllMapper.ToModel(order);
        }

        public async Task<OrderModel> PlaceServiceOrderAsync(string clientName,
            string clientEmail, string requirements, IEnumerable<int> serviceIds)
        {
            var ids = serviceIds.ToList();
            if (!ids.Any())
                throw new ArgumentException("Потрібно обрати хоча б одну послугу.");

            var order = new Order
            {
                ClientName = clientName,
                ClientEmail = clientEmail,
                Requirements = requirements,
                Type = OrderType.ServiceBased,
                Status = OrderStatus.Pending,
                CreatedAt = DateTime.UtcNow,
                OrderServices = ids.Select(id => new DALEntities.OrderService { ServiceId = id }).ToList()
            };

            await _uow.Orders.AddAsync(order);
            await _uow.SaveAsync();

            var created = await _uow.Orders.GetByIdWithDetailsAsync(order.Id);
            return DalToBllMapper.ToModel(created!);
        }

        public async Task<OrderModel> PlaceTurnkeyOrderAsync(string clientName,
            string clientEmail, string requirements)
        {
            if (string.IsNullOrWhiteSpace(requirements))
                throw new ArgumentException("Опис вимог обов'язковий для замовлення «під ключ».");

            var order = new Order
            {
                ClientName = clientName,
                ClientEmail = clientEmail,
                Requirements = requirements,
                Type = OrderType.Turnkey,
                Status = OrderStatus.Pending,
                CreatedAt = DateTime.UtcNow
            };

            await _uow.Orders.AddAsync(order);
            await _uow.SaveAsync();

            return DalToBllMapper.ToModel(order);
        }

        public async Task UpdateOrderStatusAsync(int orderId, OrderStatusModel newStatus)
        {
            var order = await _uow.Orders.GetByIdAsync(orderId)
                ?? throw new InvalidOperationException($"Замовлення #{orderId} не знайдено.");

            // бізнес-правило: скасувати можна тільки Pending
            if (newStatus == OrderStatusModel.Cancelled
                && order.Status != OrderStatus.Pending)
                throw new InvalidOperationException("Скасувати можна лише очікуюче замовлення.");

            order.Status = (OrderStatus)newStatus;
            _uow.Orders.Update(order);
            await _uow.SaveAsync();
        }
    }
}