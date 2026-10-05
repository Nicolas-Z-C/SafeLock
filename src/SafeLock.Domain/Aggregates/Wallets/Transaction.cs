using SafeLock.Domain.Common.Entities;
using SafeLock.Domain.Common.Enums;
using SafeLock.Domain.Common.Result;

namespace SafeLock.Domain.Aggregates.Wallet
{
    public class Transaction : AuditableEntity
    {
        public Guid WalletId {get; private set;}
        public Guid UserId {get; private set;}
        public decimal Amount {get; private set;}
        public TransactionType TransactionType {get; private set;}

        private Transaction() {}

        private Transaction(
            Guid walletId,
            Guid userId,
            decimal amount,
            TransactionType type
        )
        {
            WalletId = walletId;
            UserId = userId;
            Amount = amount;
            TransactionType = type;
        }

        public static ResultGen<Transaction> Register(Guid userId, Guid walletid, decimal amount, TransactionType type) =>
        ResultGen<Transaction>.Success(new Transaction(walletid,userId,amount,type));

    }
}