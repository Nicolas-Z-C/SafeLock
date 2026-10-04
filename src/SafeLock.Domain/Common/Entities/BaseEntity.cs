using SafeLock.Domain.Interfaces.Events;

namespace SafeLock.Domain.Common.Entities
{
    public abstract class BaseEntity
    {
        private readonly List<IDomainEvent> _events = [];
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _events.AsReadOnly();

        public Guid Id { get; protected set; }

        protected BaseEntity() => Id = Guid.NewGuid();

        protected void AddDomainEvent(IDomainEvent domainEvent) => _events.Add(domainEvent);
        public void ClearEvents() => _events.Clear();
    }
}