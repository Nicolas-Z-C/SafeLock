using SafeLock.Domain.Common.Result;

namespace SafeLock.Domain.Common.ValueObjects
{
    public sealed class PasswordHash : IEquatable<PasswordHash>
    {
        public string Valor { get; }

        private PasswordHash(string valor) => Valor = valor;
        public static ResultGen<PasswordHash> Create(string valor)
        {
            if (string.IsNullOrWhiteSpace(valor))
                return ResultGen<PasswordHash>.Failure(new Error("Hash.Vacio","La Hash no puede estar vacia"));

            if (!RegexPatterns.Argon2Hash().IsMatch(valor))
                return ResultGen<PasswordHash>.Failure(new Error("Hash.FormatoIncorrecto","La hash no tiene el formato correcto"));
            
            return ResultGen<PasswordHash>.Success(new PasswordHash(valor));
        }
        public bool Equals(PasswordHash? other) => other is not null && Valor == other.Valor;
        public override bool Equals(object? obj) => Equals(obj as PasswordHash);
        public override int GetHashCode() => Valor.GetHashCode();

    }
}