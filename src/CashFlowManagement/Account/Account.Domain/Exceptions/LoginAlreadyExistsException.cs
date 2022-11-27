namespace Account.Domain.Exceptions;

public class LoginUnavailable : DomainException
{
    public LoginUnavailable() : base("Login already in use.")
    { }
}
