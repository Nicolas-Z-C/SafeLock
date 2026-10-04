using SafeLock.Domain.Common.Entities;
using SafeLock.Domain.Common.Events;
using SafeLock.Domain.Common.Result;

namespace SafeLock.Domain.Aggregates.Wallet
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
                return ResultGen<Wallet>.Failure(new Error("Wallet.Usurio","El user id esta vacio"));
            
            return ResultGen<Wallet>.Success(new Wallet(userId));
        }

        //Metodos propios del objeto

        public Result Debitar(decimal amount)
        {
            if(amount < 0)
                return Result.Failure(new Error("Wallet.Debito","El saldo a debitar es menor que 0"));
            
            if(amount > Balance)
                return Result.Failure(new Error("Wallet.Saldo insuficiente","El saldo a debitar es mayor que el balance actual"));
            
            Balance -= amount;
            AddDomainEvent(new DebitEntry(UserId,Id,amount, DateTime.UtcNow));
            return Result.Success();
        }

        public Result Accredit(decimal amount)
        {
            if(amount < 0)
                return Result.Failure(new Error("Wallet.Abono","El saldo a abonar es menor que 0"));
            
            if(amount > 100000)
                return Result.Failure(new Error("Wallet.Abono","El saldo a abonar es mayor que el limite de la wallet"));
            
            Balance += amount;
            AddDomainEvent(new AccreditEntry(UserId,Id,amount, DateTime.UtcNow));
            return Result.Success();
        }
    }
}