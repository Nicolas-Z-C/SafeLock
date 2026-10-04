namespace SafeLock.Domain.Common.DomainExceptions
{
    public abstract class DomainException(string message)  : Exception(message) { }
}