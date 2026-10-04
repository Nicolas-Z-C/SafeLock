namespace SafeLock.Domain.Common.DomainExceptions
{
    public class Active : DomainException
    {
        public Active() : base("El usuario se encuentra activo") {}
    }
}