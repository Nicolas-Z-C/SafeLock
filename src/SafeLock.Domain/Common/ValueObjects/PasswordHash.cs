using SafeLock.Domain.Common.Errors;
using SafeLock.Domain.Common.Results;

namespace SafeLock.Domain.Common.ValueObjects
{
    public sealed class PasswordHash : IEquatable<PasswordHash>
    {
        public string Value { get; }

        private PasswordHash(string value) => Value = value;
        public static ResultGen<PasswordHash> Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return ResultGen<PasswordHash>.Failure(HashErrors.EmptyHash);

            if (!RegexPatterns.Argon2Hash().IsMatch(value))
                return ResultGen<PasswordHash>.Failure(HashErrors.IncorrectHashFormat);
            
            return ResultGen<PasswordHash>.Success(new PasswordHash(value));
        }
        public bool Equals(PasswordHash? other) => other is not null && Value == other.Value;
        public override bool Equals(object? obj) => Equals(obj as PasswordHash);
        public override int GetHashCode() => Value.GetHashCode();

    }
}