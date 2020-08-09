namespace SupermarketApi.Mapping
{
    using System.Net;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using SupermarketApi.Errors;
    using static System.Net.HttpStatusCode;

    [TestClass]
    public sealed class ApiResponseFromHttpStatusCodeBuilderTests
    {
        [DataTestMethod]
        [DataRow(BadGateway)]
        [DataRow(BadRequest)]
        [DataRow(InternalServerError)]
        [DataRow(OK)]
        [DataRow((HttpStatusCode)399)]
        public void BuildingApiResponseShouldReturnApiResponseCorrectly(HttpStatusCode statusCode)
        {
            // Arrange
            IBuilder<HttpStatusCode, ApiResponse> builder = new ApiResponseFromHttpStatusCodeBuilder();

            // Act
            var apiResponse = builder.Build(statusCode);

            // Assert
            _ = apiResponse.StatusCode.Should().Be(statusCode);
        }
    }
}
