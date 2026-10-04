using SafeLock.Domain.Common.Result;

namespace SafeLock.Domain.Common.ValueObjects
{
    public sealed class Email : BaseVO
    {
        private const int Length = 50;
        
        private Email(string value) : base(value) {}

        public static ResultGen<Email> Create(string value) =>
        Create(value, Length, RegexPatterns.BasicText(), v => new Email(v));
    }
}