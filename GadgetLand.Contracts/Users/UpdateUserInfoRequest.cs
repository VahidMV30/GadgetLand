namespace GadgetLand.Contracts.Users;

public record UpdateUserInfoRequest(int? CityId, string FullName, string Mobile, string PostalCode, string Address);
