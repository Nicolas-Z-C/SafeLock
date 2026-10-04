using SafeLock.Domain.Interfaces.Events;

namespace SafeLock.Domain.Common.Events
{
    public sealed record DebitEntry(
        Guid UserId,
        Guid WalletId,
        decimal Amount,
        DateTime UtcOcurredAt
    ) : IDomainEvent{}
}