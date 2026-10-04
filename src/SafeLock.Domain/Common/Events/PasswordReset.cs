using SafeLock.Domain.Common.ValueObjects;
using SafeLock.Domain.Interfaces.Events;

namespace SafeLock.Domain.Common.Events
{
    public sealed record PasswordReset(
        Guid UserId,
        Email UserEmail,
        string Token,
        DateTime UtcExpiration,
        DateTime UtcOcurredAt
    ) : IDomainEvent {}
}