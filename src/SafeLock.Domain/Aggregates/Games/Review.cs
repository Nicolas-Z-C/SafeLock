using SafeLock.Domain.Common.Entities;
using SafeLock.Domain.Common.Errors;
using SafeLock.Domain.Common.Results;
using SafeLock.Domain.Common.ValueObjects;

namespace SafeLock.Domain.Aggregates.Games
{
    public class Review : AuditableEntity
    {
        public bool IsActive {get; private set;} = true;
        public bool IsRecomended {get; private set;}
        public Guid UserId {get; private set;}
        public Guid GameId {get; private set;}
        public Description? ReviewDescription {get; private set;}

        private Review() {}

        private Review(
            bool vote,
            Guid user,
            Guid game,
            Description description
        )
        {
            IsRecomended = vote;
            UserId = user;
            GameId = game;
            ReviewDescription = description;
        }

        public static ResultGen<Review> Create(
            bool vote,
            Guid user,
            Guid game,
            string description
        )
        {
            if(user == Guid.Empty)
                return ResultGen<Review>.Failure(ReviewErrors.EmptyUserID);

            if(game == Guid.Empty)
                return ResultGen<Review>.Failure(ReviewErrors.EmptyGameID);
            
            var result = Description.Create(description);

            if(result.IsFailure)
                return ResultGen<Review>.Failure(result.Errors);

            return ResultGen<Review>.Success(new (vote,user,game,result.Value));
        }
    }
}