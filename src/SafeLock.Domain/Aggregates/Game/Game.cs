using SafeLock.Domain.Common.Entities;
using SafeLock.Domain.Common.Result;
using SafeLock.Domain.Common.ValueObjects;

namespace SafeLock.Domain.Aggregates.Game
{
    public class Game : AuditableEntity
    {
        public ImageUrl Image1 {get; private set;}
        public ImageUrl? Image2 {get; private set;}
        public decimal BasePrice {get; private set;}
        public decimal? DiscountPercentage {get; private set;}
        public Description Description {get; private set;}
        public Name AutorName {get; private set;}
        public Requirements Requirements {get; private set;}
        public DateTime? DiscountStartUtc { get; private set; }
        public DateTime? DiscountEndUtc { get; private set; }

        public decimal Price
        {
            get
            {
                bool OnDiscount = DiscountPercentage is not null
                && DiscountStartUtc <= DateTime.UtcNow
                && DiscountEndUtc >= DateTime.UtcNow;

                return OnDiscount ?
                    BasePrice - (DiscountPercentage!.Value/100m * BasePrice)
                    : BasePrice;
            }
        }

        private Game() {}

        private Game(ImageUrl image1,
        ImageUrl image2,
        decimal price,
        Description description,
        Name autorsName,
        Requirements requirements)
        {
            Image1 = image1;
            Image2 = image2;
            BasePrice = price;
            Description = description;
            AutorName = autorsName;
            Requirements = requirements;
        }

        public static ResultGen<Game> Create(
            string img1,
            string img2,
            decimal price,
            string description,
            string autor,
            string requirements
        )
        {
            List<Error> errors = [];
            List<Result> results = [];

            var image1 = ImageUrl.Create(img1);
            results.Add(image1);

            var image2 = ImageUrl.Create(img2);
            results.Add(image2);

            var gameDescription = Description.Create(description);
            results.Add(gameDescription);

            var autorName = Name.Create(autor);
            results.Add(autorName);

            var gamerequirements = Requirements.Create(requirements);
            results.Add(gamerequirements);

            foreach (var result in results)
            {
                if(result.IsFailure)
                    foreach (var error in result.Errors)
                    {
                        errors.Add(error);
                    }
            }

            if(price < 0)
                errors.Add(new Error("Game.Precio","El precio del juego es menor que 0"));

            if(errors.Count != 0)
                return ResultGen<Game>.Failure(errors);
            
            return ResultGen<Game>.Success(new Game(
                image1.Value,
                image2.Value,
                price,
                gameDescription.Value,
                autorName.Value,
                gamerequirements.Value
            ));
        }

        //Game own Methods
        
        public Result ScheduleDiscount(int percentage, DateTime StartUtc, DateTime EndUtc)
        {
            if (percentage is < 0 or > 100)
            return ResultGen<bool>.Failure(new Error("Game.Descuento","Descuento en rango invalido"));

            if (EndUtc <= StartUtc)
                return ResultGen<bool>.Failure(new Error("Game.Descuento", "Descuento en tiempos invalidos"));

            DiscountPercentage = percentage;
            DiscountStartUtc = StartUtc;
            DiscountEndUtc = EndUtc;

            return Result.Success();
        }

        public Result CancelDiscount()
        {
            if (DiscountPercentage is null)
                return Result.Failure(new Error("Game.Descuento","No hay descuentos activos"));

            DiscountPercentage = null;
            DiscountStartUtc = null;
            DiscountEndUtc = null;

            return Result.Success();
        }
    }
}