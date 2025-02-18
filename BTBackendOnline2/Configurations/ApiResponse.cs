using System.Text.Json.Serialization;

namespace BTBackendOnline2.Configurations
{
    public class ApiResponse<T>
    {
        public int Code { get; }
        public string? Message { get; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public T? Data { get; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Error { get; }

        public ApiResponse(int code, string messsage, T? data, string? error)
        {
            Code = code;
            Message = messsage;
            Data = data;
            Error = error;
        }

        public static ApiResponse<T> Success(T? data = default)
        {
            return new ApiResponse<T>(0, "Success", data, null);
        }

        public static ApiResponse<T> Fail(string? error)
        {
            return new ApiResponse<T>(1, "Error", default, error);
        }

        public static ApiResponse<T> Unauthorized(string message)
        {
            return new ApiResponse<T>(2, "Error", default, message);
        }
    }
}
