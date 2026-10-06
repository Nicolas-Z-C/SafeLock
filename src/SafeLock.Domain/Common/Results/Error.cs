using SafeLock.Domain.Common.Enums;

namespace SafeLock.Domain.Common.Results
{
    public sealed record Error(string Codigo, string  Mensaje, ErrorType ErrorType = ErrorType.Validation)
    {
        public static Error None => new(string.Empty,string.Empty);
    }
}