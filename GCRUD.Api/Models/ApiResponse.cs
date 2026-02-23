namespace GCRUD.Api.Models
{
    public class ApiResponse<T>
    {
        public int Code { get; set; }           // 0 = success, 1 = error
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }

        public static ApiResponse<T> Success(T? data, string message = "Request completed successfully")
            => new ApiResponse<T> { Code = 0, Message = message, Data = data };

        public static ApiResponse<T> Error(string message, T? data = default)
            => new ApiResponse<T> { Code = 1, Message = message, Data = data };
    }
}