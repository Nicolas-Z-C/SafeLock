using SafeLock.Domain.Common.Entities;
using SafeLock.Domain.Common.Results;
using SafeLock.Domain.Common.ValueObjects;

namespace SafeLock.Domain.Aggregates.Games
{
    public class Game : AuditableEntity
    {
        private readonly List<ImageUrl> _images = [];
        public IReadOnlyCollection<ImageUrl> Images => _images.AsReadOnly();
        private readonly List<VideoUrl> _videos = [];
        public IReadOnlyCollection<VideoUrl> Videos => _videos.AsReadOnly();

        public decimal BasePrice {get; private set;}
        public decimal? DiscountPercentage {get; private set;}
        public Description Description {get; private set;}
        public Name Developer {get; private set;}
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

        private Game(List<ImageUrl> imgs,
        List<VideoUrl> videos,
        decimal price,
        Description description,
        Name developer,
        Requirements requirements)
        {
            _images = imgs;
            _videos = videos;
            BasePrice = price;
            Description = description;
            Developer = developer;
            Requirements = requirements;
        }

        public static ResultGen<Game> Create(
            List<string> imgs,
            List<string> videos,
            decimal price,
            string description,
            string developer,
            string requirements
        )
        {   
    
            var imagesResults = imgs.Select(ImageUrl.Create).ToList();
            var videosResults = videos.Select(VideoUrl.Create).ToList();
            var gameDescription = Description.Create(description);
            var developerName = Name.Create(developer);
            var gameRequirements = Requirements.Create(requirements);

            var all = imagesResults
                        .Cast<Result>()
                        .Concat(videosResults.Cast<Result>())
                        .Append(gameDescription)
                        .Append(developerName)
                        .Append(gameRequirements)
                        .ToArray();

            var combined = Result.Combine(all);

            var errors = new List<Error>(combined.Errors);

            if (price < 0)
                errors.Add(new Error("Game.Precio","Precio invalido"));

            if (errors.Count != 0)
                return ResultGen<Game>.Failure(errors);

            
            return ResultGen<Game>.Success(new Game(
                [.. imagesResults.Select(x => x.Value)],
                [.. videosResults.Select(x => x.Value)],
                price,
                gameDescription.Value,
                developerName.Value,
                gameRequirements.Value
            ));
        }

        //Game own Methods
        
        public Result ScheduleDiscount(int percentage, DateTime startUtc, DateTime endUtc)
        {
            if (percentage is < 0 or > 100)
            return Result.Failure(new Error("Game.Descuento","Descuento en rango invalido"));

            if (endUtc <= startUtc)
                return Result.Failure(new Error("Game.Descuento", "Descuento en tiempos invalidos"));

            DiscountPercentage = percentage;
            DiscountStartUtc = startUtc;
            DiscountEndUtc = endUtc;

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