using AutoMapper;
using Magalog.Application.Dtos;
using Magalog.Application.Services.Interfaces;
using Magalog.Domain.Entitites;
using Magalog.Domain.Interfaces.Repositories;

namespace Magalog.Application.Services;

public class LegacyProcessingService : ILegacyProcessingService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public LegacyProcessingService(IOrderRepository orderRepository,
                                   IUserRepository userRepository,
                                   IMapper mapper)
    {
        _orderRepository = orderRepository;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public virtual async Task ProcessLegacyFile(string? lines)
    {
        try
        {
            var reg = lines?.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);

            var orderItems = new List<OrderItem>();
            var users = new Dictionary<int, User>();
            var orders = new Dictionary<int, Order>();

            foreach (var line in reg)
            {
                var orderItem = new OrderItem
                {
                    Product_id = int.Parse(line.Substring(65, 10).Trim()),
                    Order_id = int.Parse(line.Substring(55, 10).Trim()),
                    Value = decimal.Parse(line.Substring(75, 12).Trim())
                };
                orderItems.Add(orderItem);

                var orderId = int.Parse(line.Substring(55, 10).Trim());
                if (!orders.ContainsKey(orderId))
                {
                    orders[orderId] = new Order
                    {

                        Order_id = orderId,
                        User_id = int.Parse(line.Substring(0, 10).Trim()),
                        Date = DateOnly.ParseExact(line.Substring(87, 8), "yyyyMMdd", null),
                        OrderItems = new List<OrderItem>()
                    };
                }
                orders[orderId].OrderItems.Add(orderItem);
                orders[orderId].Total = orders[orderId].OrderItems.Where(x => x.Order_id == orderId).Sum(x => x.Value);

                var userId = int.Parse(line.Substring(0, 10).Trim());
                if (!users.ContainsKey(userId))
                {
                    users[userId] = new User
                    {
                        User_Id = userId,
                        Name = line.Substring(10, 45).Trim()
                    };
                }
            }

            await _userRepository.Add(users.Values.ToList());
            await _orderRepository.Add(orders.Values.ToList());
        }
        catch (Exception ex)
        {
            throw new Exception("Error processing legacy file", ex);
        }
    }


    public virtual async Task<IEnumerable<UserDto>> GetOrders(int? order_id, DateOnly? startDate, DateOnly? endDate)
    {
        try
        {
            var orders = await _orderRepository.GetOrders(order_id, startDate, endDate);
            var users = new List<UserDto>();
            foreach (var order in orders)
            {
                var userDto = new UserDto();
                userDto.User_Id = order.User.User_Id;
                userDto.Name = order.User.Name;
                userDto.Orders.Add(_mapper.Map<OrderDto>(order));

                users.Add(userDto);
            }

            return users;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}