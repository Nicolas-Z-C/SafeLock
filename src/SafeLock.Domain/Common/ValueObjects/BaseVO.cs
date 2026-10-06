using System.Text.RegularExpressions;
using SafeLock.Domain.Common.Errors;
using SafeLock.Domain.Common.Results;

namespace SafeLock.Domain.Common.ValueObjects
{
    public abstract class BaseVO(string value) : IEquatable<BaseVO> 
    {
        public string Value { get; } = value;

        protected static ResultGen<T> Create<T>(
            string value,
            int maxLength,
            Regex regex,
            Func<string, T> factory) where T : BaseVO
        {
            var errors = new List<Error>();

            if (string.IsNullOrWhiteSpace(value))
                errors.Add(VOErrors.EmptyValue);

            var valorLimpio = value?.Trim() ?? string.Empty;

            if (valorLimpio.Length > maxLength)
                errors.Add(VOErrors.MaxLenghtExceeded);

            if (!string.IsNullOrWhiteSpace(valorLimpio) && !regex.IsMatch(valorLimpio))
                errors.Add(VOErrors.RegexNotMatched);

            return errors.Any()
                ? ResultGen<T>.Failure(errors)
                : ResultGen<T>.Success(factory(valorLimpio));
        }

        public override bool Equals(object? obj) => Equals(obj as BaseVO);
        public bool Equals(BaseVO? other) =>
            other is not null && GetType() == other.GetType() && Value == other.Value;
        public override int GetHashCode() => HashCode.Combine(GetType(), Value);
        public override string ToString() => Value;
    }
}