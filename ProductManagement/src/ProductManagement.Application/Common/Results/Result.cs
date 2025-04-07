namespace ProductManagement.Application.Common.Results
{

    public class Result<T> : Result
    {
        public T Value { get; }

        private Result(T value) : base(true, string.Empty)
        {
            Value = value;
        }

        private Result(string error) : base(false, error)
        {
        }

        public static Result<T> Success(T value) => new Result<T>(value);

        public new static Result<T> Failure(string error) => new Result<T>(error);
    }

    public class Result
    {
        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
        public string Error { get; }

        protected Result(bool isSuccess, string error)
        {
            if (isSuccess && error != string.Empty)
                throw new InvalidOperationException("O resultado do sucesso não deve ter erro.");

            if (!isSuccess && error == string.Empty)
                throw new InvalidOperationException("O resultado da falha deve ter uma mensagem de erro.");

            IsSuccess = isSuccess;
            Error = error;
        }

        public static Result Success() => new Result(true, string.Empty);

        public static Result Failure(string error) => new Result(false, error);
    }
}
