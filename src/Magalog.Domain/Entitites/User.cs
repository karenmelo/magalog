namespace Magalog.Domain.Entitites;

public class User
{
    public int User_Id { get; set; }
    public string Name { get; set; }

    public ICollection<Order> Orders { get; set; } = new List<Order>();
}
