namespace GadgetLand.Domain.Entities;

public class Review
{
    public int Id { get; set; }

    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;

    public double Rating { get; set; }
    public string Comment { get; set; } = string.Empty;
    public bool IsConfirmed { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public bool IsDeleted { get; set; }
}
