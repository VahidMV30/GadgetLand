namespace GadgetLand.Contracts.Auth;

public record RegisterResponse(AuthResponse AuthResponse, string Token)
{
    public RegisterResponse() : this(null!, string.Empty)
    {

    }
}
