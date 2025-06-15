namespace GadgetLand.Contracts.Users;

public record UpdateUserAddressInfoRequest(int? CityId, string FullName, string Mobile, string PostalCode, string Address);
