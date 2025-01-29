using Magalog.Data.Context;
using Magalog.Domain.Entitites;
using Magalog.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Magalog.Data.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly AppDbContext _context;
    public OrderRepository(AppDbContext context) => _context = context;



    public async Task Add(List<Order> orders)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            foreach (var order in orders)
            {

                var orderExist = await _context.Orders
                                               .Include(o => o.OrderItems)
                                               .AsNoTracking()
                                               .FirstOrDefaultAsync(x => x.Order_id == order.Order_id &&
                                                                         x.Date == order.Date);
                
                if (orderExist != null)
                {
                    order.Total += orderExist.Total;
                    _context.Orders.Update(order);
                }
                else
                    await _context.Orders.AddAsync(order);
            }

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<IEnumerable<Order>> GetOrders(int? order_id, DateOnly? startDate, DateOnly? endDate)
    {
        try
        {
            var query = _context.Orders.AsQueryable();

            if (order_id != null)
                query = query.Where(o => o.Order_id == order_id);

            if (startDate != null && endDate != null)
                query = query.Where(o => o.Date >= startDate.Value && o.Date <= endDate.Value);


            var result = await query.Include(u => u.User)
                                    .Include(o => o.OrderItems)
                                    .AsNoTracking()
                                    .ToListAsync();

            return result;
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }
}
