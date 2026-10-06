using SafeLock.Domain.Common.Enums;
using SafeLock.Domain.Common.Results;

namespace SafeLock.Domain.Common.Errors
{
    public class HashErrors
    {
        
        public static readonly Error EmptyHash =
        new ("Hash.Empty","The hash is empty",ErrorType.Exception);

        public static readonly Error IncorrectHashFormat =
        new ("Hash.Format","The hash is not on the correct format",ErrorType.Exception);
    }
}