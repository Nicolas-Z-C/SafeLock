using SafeLock.Domain.Common.Enums;
using SafeLock.Domain.Common.Results;

namespace SafeLock.Domain.Common.Errors
{
    public static class BaseUserErrors
    {
        
        public static readonly Error UserInactive = 
        new ("User.Inactive","The user is inactive",ErrorType.Exception);

        public static readonly Error UserEmptyToken = 
        new ("User.Token","The token provided is empty", ErrorType.Exception);

        public static readonly Error UserAlreadyActive = 
        new ("User.Active","The user is already active", ErrorType.Exception);

        public static readonly Error InvalidResetToken = 
        new ("User.ResetToken","The token is invalid");
        
    }
}