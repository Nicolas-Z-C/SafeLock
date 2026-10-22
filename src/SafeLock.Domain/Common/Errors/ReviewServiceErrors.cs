using SafeLock.Domain.Common.Results;

namespace SafeLock.Domain.Common.Errors
{
    public static class ReviewServiceErrors
    {
        
        public static readonly Error GameNotOwned = 
        new ("ReviewService.Game","The user does not have this game");

        public static readonly Error AlreadyReviewed = 
        new ("ReviewService.Game","The user has already reviewded this game");

    }
}