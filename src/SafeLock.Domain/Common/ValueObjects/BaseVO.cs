using System.Text.RegularExpressions;
using SafeLock.Domain.Common.Result;

namespace SafeLock.Domain.Common.ValueObjects
{
    public abstract class BaseVO : IEquatable<BaseVO>
    {
        public string Value { get; }

        protected BaseVO(string value)
        {
            Value = value;
        }

        protected static ResultGen<T> Create<T>(
            string value,
            int maxLength,
            Regex regex,
            Func<string, T> factory) where T : BaseVO
        {
            var errors = new List<Error>();

            if (string.IsNullOrWhiteSpace(value))
                errors.Add(new Error("DominioVO.Vacio", "El valor no puede estar vacío o en blanco."));

            var valorLimpio = value?.Trim() ?? string.Empty;

            if (valorLimpio.Length > maxLength)
                errors.Add(new Error("DominioVO.Longitud", $"Ha excedido la longitud máxima de {maxLength}"));

            if (!string.IsNullOrWhiteSpace(valorLimpio) && !regex.IsMatch(valorLimpio))
                errors.Add(new Error("DominioVO.Regex", "El valor registrado no cuenta con el formato adecuado"));

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