using SafeLock.Domain.Interfaces.Events;

namespace SafeLock.Domain.Common.Events
{
    public sealed record WalletDebited(
        Guid UserId,
        Guid WalletId,
        decimal Amount,
        DateTime UtcOccurredAt 
    ) : IDomainEvent{}
}