namespace SafeLock.Domain.Common.Results
{
    public sealed record Error(string Codigo, string  Mensaje)
    {
        public static Error None => new(string.Empty,string.Empty);
    }
}