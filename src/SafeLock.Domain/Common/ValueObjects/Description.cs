using SafeLock.Domain.Common.Result;

namespace SafeLock.Domain.Common.ValueObjects
{
    public sealed class Description : BaseVO
    {
        private const int Length = 10000;
        
        private Description(string value) : base(value) {}

        public static ResultGen<Description> Create(string value) =>
        Create(value, Length, RegexPatterns.BasicText(), v => new Description(v));
    }
}