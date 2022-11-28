using System.Runtime.Serialization;

namespace Account.Domain.Exceptions;


public class DomainException : Exception
{
    public DomainException()
    { }

    public DomainException(string message) : base(message)
    { }

    public DomainException(SerializationInfo info, StreamingContext context) : base(info, context)
    { }

    public DomainException(string message, Exception innerException) : base(message, innerException)
    { }
}
