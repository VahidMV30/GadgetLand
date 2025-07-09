using GadgetLand.Contracts.Orders;

namespace GadgetLand.Contracts.Users;

public class UserDetailsWithOrdersResponse
{
    public UserDetailsResponse User { get; set; } = null!;
    public List<OrderDetailsResponse> Orders { get; set; } = [];
}
