using SafeLock.Domain.Common.Entities;
using SafeLock.Domain.Common.Errors;
using SafeLock.Domain.Common.Events;
using SafeLock.Domain.Common.Results;

namespace SafeLock.Domain.Aggregates.Wallets
{
    public class Wallet : AuditableEntity
    {
        
        public Guid UserId {get; private set;}
        public decimal Balance {get; private set;} = 100000;

        private Wallet() {}
        private Wallet(Guid userId) => UserId = userId;

        public static ResultGen<Wallet> Create(Guid userId)
        {
            if(userId.Equals(Guid.Empty))
                return ResultGen<Wallet>.Failure(WalletErrors.EmptyUserID);
            
            return ResultGen<Wallet>.Success(new Wallet(userId));
        }

        //Metodos propios del objeto

        public Result Debitar(decimal amount)
        {
            if(amount < 0)
                return Result.Failure(WalletErrors.NegativeAmountDebit);
            
            if(amount > Balance)
                return Result.Failure(WalletErrors.InsufficientBalance);
            
            Balance -= amount;
            AddDomainEvent(new WalletDebited(UserId,Id,amount, DateTime.UtcNow));
            return Result.Success();
        }

        public Result Credit(decimal amount)
        {
            if(amount < 0)
                return Result.Failure(WalletErrors.negativeAccredit);
            
            if(amount > 100000)
                return Result.Failure(WalletErrors.AccreditLimitExceeded);
            
            if(Balance + amount > 100000)
                return Result.Failure(WalletErrors.WalletLimit);
            Balance += amount;
            AddDomainEvent(new WalletCredited(UserId,Id,amount, DateTime.UtcNow));
            return Result.Success();
        }
    }
}