﻿namespace GadgetLand.Domain.Entities;

public class Province
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public ICollection<City> Cities { get; set; } = [];
}
