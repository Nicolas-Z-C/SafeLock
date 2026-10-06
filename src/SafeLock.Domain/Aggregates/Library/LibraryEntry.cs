using SafeLock.Domain.Common.Entities;
using SafeLock.Domain.Common.Errors;
using SafeLock.Domain.Common.Results;

namespace SafeLock.Domain.Aggregates.Library
{
    public class LibraryEntry : AuditableEntity
    {
        public Guid UserId {get; private set;}
        public Guid GameId {get; private set;}
        public DateTime BoughtDate {get; private set; } = DateTime.UtcNow;

        private LibraryEntry() {}

        private LibraryEntry(Guid user, Guid game)
        {
            UserId = user;
            GameId = game;
        }

        public static ResultGen<LibraryEntry> Create(Guid user, Guid game)
        {
            if(user == Guid.Empty)
                return ResultGen<LibraryEntry>.Failure(LibraryEntryErrors.EmptyUserID);

            if(game == Guid.Empty)
                return ResultGen<LibraryEntry>.Failure(LibraryEntryErrors.EmptyGameID);
            
            return ResultGen<LibraryEntry>.Success(new LibraryEntry(user, game));
        }
    }
}