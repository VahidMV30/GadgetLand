namespace GadgetLand.Contracts.Auth;

public record LoginResponse(AuthResponse AuthResponse, string Token)
{
    public LoginResponse() : this(null!, string.Empty)
    {

    }
}
