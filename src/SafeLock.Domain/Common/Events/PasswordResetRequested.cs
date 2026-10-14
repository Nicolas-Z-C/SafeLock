using System.Text;
using SafeLock.Domain.Common.ValueObjects;
using SafeLock.Domain.Interfaces.Events;

namespace SafeLock.Domain.Common.Events
{
    public sealed record PasswordResetRequested(
        Guid UserId,
        Email UserEmail,
        string Token,
        DateTime UtcExpiration,
        DateTime UtcOccurredAt 
    ) : IDomainEvent {

    private bool PrintMembers(StringBuilder builder)
        {
            builder.Append($"UserId = {UserId}, UtcExpiration = {UtcExpiration:O}");
            return true;
        }
    }
}