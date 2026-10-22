
using SafeLock.Domain.Common.Enums;
using SafeLock.Domain.Common.Results;

namespace SafeLock.Domain.Common.Errors
{
    public static class GameErrors
    {
        
        public static readonly Error InvalidPrice =
        new ("Game.Price","Invalid price",ErrorType.Validation);

        public static readonly Error NoImages =
        new ("Game.Images","No images where found",ErrorType.Validation);

        public static readonly Error DiscountPercentageInvalid =
        new ("Game.Discount","Percentage not in valid range",ErrorType.Validation);

        public static readonly Error DiscountTimeInvalid =
        new ("Game.Discount","Invalid range for the discount",ErrorType.Validation);

        public static readonly Error NotOnDiscount =
        new ("Game.DiscountCancel","The game is not on discount",ErrorType.Exception);


    }
}