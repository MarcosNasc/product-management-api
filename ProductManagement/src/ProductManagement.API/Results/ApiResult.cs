namespace ProductManagement.API.Results
{
    public class ApiResult
    {
        public bool Success { get; init; }
        public string? Message { get; init; }
        public List<string>? Errors { get; init; }  

        public static ApiResult Ok(string? message = null) =>
            new() { Success = true, Message = message };

        public static ApiResult Fail(string message) =>
            new() { Success = false, Message = message };

        public static ApiResult Fail(List<string> errors) =>
            new() { Success = false, Errors = errors };
    }

    public class ApiResult<T> : ApiResult
    {
        public T? Data { get; init; }

        public static ApiResult<T> Ok(T data, string? message = null) =>
            new() { Success = true, Message = message, Data = data };

        public static new ApiResult<T> Fail(string message) =>
            new() { Success = false, Message = message };

        public static ApiResult<T> Fail(List<string> errors) =>
            new() { Success = false, Errors = errors };
    }


}


