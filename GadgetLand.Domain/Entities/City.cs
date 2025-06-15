namespace GadgetLand.Domain.Entities;

public class City
{
    public int Id { get; set; }

    public int ProvinceId { get; set; }
    public Province Province { get; set; } = null!;

    public string Name { get; set; } = string.Empty;

    public ICollection<User> Users { get; set; } = [];
}
