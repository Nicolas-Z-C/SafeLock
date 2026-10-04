namespace SafeLock.Domain.Common.Result
{
    public sealed record Error(string Codigo, string  Mensaje)
    {
        public static Error None => new(string.Empty,string.Empty);
    }
}