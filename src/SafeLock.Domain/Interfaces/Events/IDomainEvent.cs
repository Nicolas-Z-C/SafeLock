namespace SafeLock.Domain.Interfaces.Events
{
    public interface IDomainEvent
    {
        DateTime UtcOccurredAt  {get ;}
    }
}