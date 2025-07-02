namespace GadgetLand.Domain.Entities;

public class Product
{
    public int Id { get; set; }

    public int CategoryId { get; set; }
    public Category Category { get; set; } = null!;

    public int BrandId { get; set; }
    public Brand Brand { get; set; } = null!;

    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public long Price { get; set; }
    public long? DiscountPrice { get; set; }
    public int QuantityInStock { get; set; }
    public string Description { get; set; } = string.Empty;

    public ICollection<ProductImage> ProductImages { get; set; } = [];
    public ICollection<Review> Reviews { get; set; } = [];
    public ICollection<PaymentItem> PaymentItems { get; set; } = [];
    public ICollection<OrderItem> OrderItems { get; set; } = [];
}
