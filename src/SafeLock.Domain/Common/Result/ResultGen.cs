namespace SafeLock.Domain.Common.Result
{
    public class ResultGen<T> : Result
    {
        private readonly T? _value;
        public T Value => IsSuccess
            ? _value!
            : throw new InvalidOperationException("No se puede acceder al valor de un resultado fallido.");

        private ResultGen(T value) : base(true, [])
        {
            _value = value;
        }

        private ResultGen(IReadOnlyList<Error> errors) : base(false, errors)
        {
            _value = default;
        }

        public static ResultGen<T> Success(T value) => new(value);

        public static new ResultGen<T> Failure(Error error) => new([error]);

        public static new ResultGen<T> Failure(IEnumerable<Error> errors) => new([.. errors]);
    }
}