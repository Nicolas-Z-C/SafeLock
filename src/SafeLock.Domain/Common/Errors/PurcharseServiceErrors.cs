using SafeLock.Domain.Common.Enums;
using SafeLock.Domain.Common.Results;

namespace SafeLock.Domain.Common.Errors
{
    public class PurcharseServiceErrors
    {
        
        public static readonly Error GameAlreadyOwned = 
        new ("PurcharseService.Game","The user already has this game",ErrorType.Exception);

        public static readonly Error EmptyGameID = 
        new ("Library.GameId","The game id is empty",ErrorType.Exception);

    }
}