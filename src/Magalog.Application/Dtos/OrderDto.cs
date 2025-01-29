using Magalog.Domain.Entitites;

namespace Magalog.Application.Dtos;

public class OrderDto
{
    public int Order_Id { get; set; }
    public decimal Total { get; set; }
    public DateOnly Date { get; set; }
    public List<OrderItemDto> Products { get; set; } = new();
}
