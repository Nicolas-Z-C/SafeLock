using SafeLock.Domain.Aggregates.Games;
using SafeLock.Domain.Common.Errors;
using SafeLock.Domain.Common.Results;

namespace SafeLock.Domain.Services
{
    public sealed class ReviewService
    {
        public ResultGen<Review> ReviewGame(
            Guid gameID,
            Guid userID,
            bool isRecommended,
            bool GameOwned,
            bool alreadyReviewed,
            string review)
        {
            
            if(!GameOwned)
                return ResultGen<Review>.Failure(ReviewServiceErrors.GameNotOwned);
            
            if(alreadyReviewed)
                return ResultGen<Review>.Failure(ReviewServiceErrors.AlreadyReviewed);
            
            return Review.Create(isRecommended,userID,gameID,review);
        
        }
    }
}