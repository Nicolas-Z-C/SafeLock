using SafeLock.Domain.Common.Enums;
using SafeLock.Domain.Common.Results;

namespace SafeLock.Domain.Common.Errors
{
    public class LibraryEntryErrors
    {
        
        public static readonly Error EmptyUserID = 
        new ("Library.UserId","The user id is empty",ErrorType.Exception);

        public static readonly Error EmptyGameID = 
        new ("Library.GameId","The game id is empty",ErrorType.Exception);
    }
}