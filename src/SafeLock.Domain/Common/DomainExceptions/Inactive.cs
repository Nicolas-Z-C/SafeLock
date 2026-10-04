namespace SafeLock.Domain.Common.DomainExceptions
{
    public class Inactive : DomainException
    {
        public Inactive() : base("El usuario se encuentra desactivado") {}
    }
}