﻿namespace GadgetLand.Domain.Entities;

public class ProductImage
{
    public int Id { get; set; }

    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;

    public string Image { get; set; } = string.Empty;
}
