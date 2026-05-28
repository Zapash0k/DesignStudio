using DesignStudio.BLL.Models;

namespace DesignStudio.BLL.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderModel>> GetAllOrdersAsync();
        Task<OrderModel?> GetOrderByIdAsync(int id);
        Task<OrderModel> PlaceServiceOrderAsync(string clientName, string clientEmail,
                                                string requirements, IEnumerable<int> serviceIds);
        Task<OrderModel> PlaceTurnkeyOrderAsync(string clientName, string clientEmail,
                                                string requirements);
        Task UpdateOrderStatusAsync(int orderId, OrderStatusModel newStatus);
    }
}