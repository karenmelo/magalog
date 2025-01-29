using Magalog.Domain.Entitites;

namespace Magalog.Domain.Interfaces.Repositories;

public interface IOrderRepository
{
    Task Add(List<Order> orders);
    Task<IEnumerable<Order>> GetOrders(int? order_id, DateOnly? startDate, DateOnly? endDate);
}
