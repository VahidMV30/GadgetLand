﻿namespace GadgetLand.Domain.Entities;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;

    public ICollection<Product> Products { get; set; } = [];
}
