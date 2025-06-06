﻿namespace GadgetLand.Contracts.Products;

public record ProductWithImagesResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string[] Images { get; set; } = [];
}
