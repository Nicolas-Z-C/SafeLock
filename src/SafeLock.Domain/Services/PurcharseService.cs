using SafeLock.Domain.Aggregates.Games;
using SafeLock.Domain.Aggregates.Library;
using SafeLock.Domain.Aggregates.Wallets;
using SafeLock.Domain.Common.Errors;
using SafeLock.Domain.Common.Results;

namespace SafeLock.Domain.Services
{
    public sealed class PurcharseService
    {
        public ResultGen<LibraryEntry> Purcharse(Wallet wallet, Game game, bool alreadyOwned)
        {
            
            if(alreadyOwned)
                return ResultGen<LibraryEntry>.Failure(PurcharseServiceErrors.EmptyGameID);

            var debit = wallet.Debit(game.Price);

            if(debit.IsFailure)
                return ResultGen<LibraryEntry>.Failure(debit.Errors);

            return LibraryEntry.Create(wallet.UserId, game.Id);

        }
    }
}