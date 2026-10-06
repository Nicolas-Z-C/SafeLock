using SafeLock.Domain.Common.Enums;
using SafeLock.Domain.Common.Results;

namespace SafeLock.Domain.Common.Errors
{
    public class WalletErrors
    {
        
        public static readonly Error EmptyUserID = 
        new ("Wallet.User","The User ID is Empty",ErrorType.Exception);

        public static readonly Error NegativeAmountDebit = 
        new ("Wallet.Debit","The debit amoun is negative", ErrorType.Exception);

        public static readonly Error InsufficientBalance = 
        new ("Wallet.Balance","The balance on the wallet is insufficient", ErrorType.Validation);

        public static readonly Error negativeAccredit = 
        new ("Wallet.Acredit","The amount provided for the accredit is negative",ErrorType.Exception);

        public static readonly Error AccreditLimitExceeded = 
        new ("Wallet.AcreditLimit","The amount provided exceeds the limit of 100.000 credits",ErrorType.Validation);

        public static readonly Error WalletLimit = 
        new ("Wallet.BalanceLimit","The amount provided exceeds the overall limit of your wallet of 100.000 credits",ErrorType.Validation);
    }
}