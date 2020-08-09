namespace SupermarketApi.Errors
{
    using System.Net;
    using SupermarketApi.Extensions;

    public class ApiResponse
    {
        public ApiResponse(HttpStatusCode statusCode, string? message = default)
        {
            this.StatusCode = statusCode;
            this.Message = message ?? GetDefaultMessageForStatusCode(statusCode);
        }

        public HttpStatusCode StatusCode { get; private set; }

        public string Message { get; private set; }

        private static string GetDefaultMessageForStatusCode(HttpStatusCode statusCode)
        {
            return statusCode.ToString().AddSpaceBeforeCapitalLetters();
        }
    }
}
