namespace SafeLock.Domain.Common.Entities
{
    public abstract class AuditableEntity : BaseEntity
    {
        public DateTime CreatedAt {get; protected set; }= DateTime.UtcNow;
        public DateTime? ModifiedAt {get; protected set;}

        protected AuditableEntity() { }

        internal void Modified() => ModifiedAt = DateTime.UtcNow;

    }
}