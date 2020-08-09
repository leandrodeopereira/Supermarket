namespace SupermarketApi.Errors
{
    using System.Net;

    public class ApiExceptionResponse : ApiResponse
    {
        public ApiExceptionResponse(HttpStatusCode statusCode, string? message = default, string? details = default)
            : base(statusCode, message)
        {
            this.Details = details;
        }

        public string? Details { get; private set; }
    }
}
