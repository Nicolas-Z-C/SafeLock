using SafeLock.Domain.Common.Enums;
using SafeLock.Domain.Common.Results;

namespace SafeLock.Domain.Common.Errors
{
    public static class DescriptionErrors
    {
        
        public static readonly Error RegexNotMatched =
        new ("VODomain.Regex","The value does not have the adecuate format",ErrorType.Validation);

        public static readonly Error MaxLenghtExceeded =
        new ("VODomain.Lenght","The leght provided is too long",ErrorType.Validation);

    }
}