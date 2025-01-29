using System.Text.Json.Serialization;

namespace Magalog.Domain.Entitites;

public class OrderItem
{

    public int Id { get; set; }
    public int Order_id { get; set; }
    public int Product_id { get; set; }
    public decimal Value { get; set; }

    public DateOnly Date { get; set; }

    //EF Relational
    public Order Order { get; set; }
}
