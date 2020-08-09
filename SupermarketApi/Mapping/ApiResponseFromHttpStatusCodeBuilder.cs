namespace SupermarketApi.Mapping
{
    using System.Net;
    using SupermarketApi.Errors;

    public sealed class ApiResponseFromHttpStatusCodeBuilder : IBuilder<HttpStatusCode, ApiResponse>
    {
        ApiResponse IBuilder<HttpStatusCode, ApiResponse>.Build(HttpStatusCode statusCode)
        {
            return new ApiResponse(statusCode);
        }
    }
}
