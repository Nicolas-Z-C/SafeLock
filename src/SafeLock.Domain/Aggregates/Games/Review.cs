using SafeLock.Domain.Common.Entities;
using SafeLock.Domain.Common.ValueObjects;

namespace SafeLock.Domain.Aggregates.Games
{
    public class Review : AuditableEntity
    {
        public bool UpVote {get; private set;}
        public Guid UserId {get; private set;}
        public Guid GameId {get; private set;}
        public Description Review
    }
}