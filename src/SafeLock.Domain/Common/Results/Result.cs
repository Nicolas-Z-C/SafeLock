namespace SafeLock.Domain.Common.Results
{
    public class Result
    {
        public bool IsSuccess {get;}
        public bool IsFailure => !IsSuccess;
        public IReadOnlyList<Error> Errors { get; }

        protected Result(bool isSuccess, IReadOnlyList<Error> errors)
        {
            if (isSuccess && errors.Any())
                throw new InvalidOperationException("Un resultado exitoso no puede tener errores.");

            if (!isSuccess && !errors.Any())
                throw new InvalidOperationException("Un resultado fallido debe tener al menos un error.");

            IsSuccess = isSuccess;
            Errors = errors;
        }

        public static Result Success() => new(true, Array.Empty<Error>());

        public static Result Failure(Error error) => new(false, [error]);

        public static Result Failure(IEnumerable<Error> errors) => new(false, [.. errors]);

        public static Result Combine(params Result[] resultados)
        {
            var errores = resultados
                .Where(r => r.IsFailure)
                .SelectMany(r => r.Errors)
                .ToList();

            return errores.Any() ? Failure(errores) : Success();
        }
    }
}