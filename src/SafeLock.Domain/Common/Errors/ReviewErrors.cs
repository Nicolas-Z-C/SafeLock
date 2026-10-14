using SafeLock.Domain.Common.Enums;
using SafeLock.Domain.Common.Results;

namespace SafeLock.Domain.Common.Errors
{
    public class ReviewErrors
    {
        public static readonly Error EmptyUserID = 
        new ("Review.UserId","The user id is empty",ErrorType.Exception);

        public static readonly Error EmptyGameID = 
        new ("Review.GameId","The game id is empty",ErrorType.Exception);

    }
}