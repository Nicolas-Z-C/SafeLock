using SafeLock.Domain.Common.ValueObjects;

namespace SafeLock.Domain.Common.Result
{
    public class Requirements : BaseVO
    {
        private const int Length = 10000;
        
        private Requirements(string value) : base(value) {}

        public static ResultGen<Requirements> Create(string value) =>
        Create(value, Length, RegexPatterns.BasicText(), v => new Requirements(v));
    }
}