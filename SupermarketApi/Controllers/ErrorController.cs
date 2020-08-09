namespace SupermarketApi.Controllers
{
    using System.Net;
    using Microsoft.AspNetCore.Mvc;
    using SupermarketApi.Errors;
    using SupermarketApi.Mapping;

    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("errors/{statuscode}")]
    public class ErrorController : ControllerBase
    {
        private readonly IBuilder<HttpStatusCode, ApiResponse> apiResponseBuilder;

        public ErrorController(IBuilder<HttpStatusCode, ApiResponse> apiResponseBuilder)
        {
            this.apiResponseBuilder = apiResponseBuilder;
        }

        public IActionResult Error(HttpStatusCode statusCode)
        {
            return new ObjectResult(this.apiResponseBuilder.Build(statusCode));
        }
    }
}
