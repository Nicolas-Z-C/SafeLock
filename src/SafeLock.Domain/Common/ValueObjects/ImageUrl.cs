using SafeLock.Domain.Common.Results;

namespace SafeLock.Domain.Common.ValueObjects
{
    public sealed class ImageUrl : BaseVO
    {
        private const int Length = 500;
        
        private ImageUrl(string value) : base(value) {}

        public static ResultGen<ImageUrl> Create(string value) =>
        Create(value, Length, RegexPatterns.Url(), v => new ImageUrl(v));
    }
}