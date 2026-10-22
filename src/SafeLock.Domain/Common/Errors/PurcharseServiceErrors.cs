using SafeLock.Domain.Common.Results;

namespace SafeLock.Domain.Common.Errors
{
    public static class PurcharseServiceErrors
    {

        public static readonly Error GameAlreadyOwned = 
        new ("PurcharseService.Game","The user already has this game");

    }
}