using SafeLock.Domain.Common.Entities;
using SafeLock.Domain.Common.Result;

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
                return ResultGen<LibraryEntry>.Failure(new Error("Libreria.UserId", "El user ID esta vacio"));

            if(game == Guid.Empty)
                return ResultGen<LibraryEntry>.Failure(new Error("Libreria.GameID", "El game ID esta vacio"));
            
            return ResultGen<LibraryEntry>.Success(new LibraryEntry(user, game));
        }
    }
}