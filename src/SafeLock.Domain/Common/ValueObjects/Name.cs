using SafeLock.Domain.Common.Result;

namespace SafeLock.Domain.Common.ValueObjects
{
    public sealed class Name : BaseVO
    {
        private const int Length = 50;
        
        private Name(string value) : base(value) {}

        public static ResultGen<Name> Create(string value) =>
        Create(value, Length, RegexPatterns.BasicText(), v => new Name(v));
    }
}
