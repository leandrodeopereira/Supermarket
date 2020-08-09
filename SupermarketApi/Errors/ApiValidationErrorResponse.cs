namespace SupermarketApi.Errors
{
    using System.Collections.Generic;
    using static System.Net.HttpStatusCode;

    public class ApiValidationErrorResponse : ApiResponse
    {
        public ApiValidationErrorResponse(IEnumerable<string> errors)
            : base(BadRequest)
        {
            this.Errors = errors;
        }

        public IEnumerable<string> Errors { get; set; }
    }
}
