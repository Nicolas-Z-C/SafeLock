using SafeLock.Domain.Interfaces.Events;

namespace SafeLock.Domain.Common.Events
{
    public sealed record WalletCredited(
        Guid UserId,
        Guid WalletId,
        decimal Amount,
        DateTime UtcOccurredAt 
    ) : IDomainEvent{}
}