namespace Magalog.Application.Dtos;

public class UserDto
{
    public int User_Id { get; set; }
    public string Name { get; set; }

    public ICollection<OrderDto> Orders { get; set; } = new List<OrderDto>();
}
