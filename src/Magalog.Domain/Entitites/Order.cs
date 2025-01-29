namespace Magalog.Domain.Entitites;

public class Order
{


    public int Order_id { get; set; }
    public decimal Total { get; set; }
    public DateOnly Date { get; set; }
    public List<OrderItem> OrderItems { get; set; } = new();
    public int User_id { get; set; }

    //EF Relational
    public User User { get; set; }

}