namespace University.Api.ErrorHandling
{
    public class ApiResponse
    {
        public ApiResponseCode ErrorCode { get; set; }
    }

    public class ApiResponse<T> : ApiResponse
    {
        public T Result { get; set; }
    }

    public class FailedApiResponse : ApiResponse
    {

        public string ErrorMessage { get; set; }

        public ValidationResult[] ValidationResult { get; set; }
    }
}
