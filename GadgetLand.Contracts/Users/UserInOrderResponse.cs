﻿namespace GadgetLand.Contracts.Users;

public record UserInOrderResponse
{
    public string FullName { get; set; } = string.Empty;
    public string Mobile { get; set; } = string.Empty;
}
