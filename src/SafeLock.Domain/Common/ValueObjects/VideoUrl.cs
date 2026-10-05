using SafeLock.Domain.Common.Result;

namespace SafeLock.Domain.Common.ValueObjects
{
    public sealed class VideoUrl : BaseVO
    {
        private const int Length = 500;
        
        private VideoUrl(string value) : base(value) {}

        public static ResultGen<VideoUrl> Create(string value) =>
        Create(value, Length, RegexPatterns.VideoUrl(), v => new VideoUrl(v));
    }
}