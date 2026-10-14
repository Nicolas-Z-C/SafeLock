using SafeLock.Domain.Common.Errors;
using SafeLock.Domain.Common.Results;

namespace SafeLock.Domain.Common.ValueObjects
{
    public sealed class Description
    {
        private const int Length = 10000;
        public string Value { get; }

        private Description(string value) => Value = value;
        public static ResultGen<Description> Create(string value)
        {

            if(value.Length > Length)
                return ResultGen<Description>.Failure(DescriptionErrors.MaxLenghtExceeded);

            if (!RegexPatterns.BasicText().IsMatch(value))
                return ResultGen<Description>.Failure(DescriptionErrors.RegexNotMatched);
            
            return ResultGen<Description>.Success(new Description(value));
        }
        public bool Equals(Description? other) => other is not null && Value == other.Value;
        public override bool Equals(object? obj) => Equals(obj as PasswordHash);
        public override int GetHashCode() => Value.GetHashCode();
    }
}