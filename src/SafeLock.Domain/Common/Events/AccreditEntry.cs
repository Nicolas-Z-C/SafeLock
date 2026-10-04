using SafeLock.Domain.Interfaces.Events;

namespace SafeLock.Domain.Common.Events
{
    public sealed record AccreditEntry(
        Guid UserId,
        Guid WalletId,
        decimal Amount,
        DateTime UtcOcurredAt
    ) : IDomainEvent{}
}