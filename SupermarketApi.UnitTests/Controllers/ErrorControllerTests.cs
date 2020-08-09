namespace SupermarketApi.Controllers
{
    using System.Net;
    using FluentAssertions;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NSubstitute;
    using SupermarketApi.Errors;
    using SupermarketApi.Mapping;
    using static System.Net.HttpStatusCode;

    [TestClass]
    public sealed class ErrorControllerTests
    {
        [TestMethod]
        public void TreatingErrorWithStatusCodeShouldReturnApiCorrectly()
        {
            // Arrange
            var statusCode = NotFound;

            var apiResponse = new ApiResponse(NotFound);

            var apiResponseBuilder = Substitute.For<IBuilder<HttpStatusCode, ApiResponse>>();
            _ = apiResponseBuilder
                .Build(statusCode)
                .Returns(apiResponse);

            var controller = new ErrorController(apiResponseBuilder);

            // Act
            var actionResult = controller.Error(statusCode);

            // Assert
            var result = actionResult as ObjectResult;
            _ = result!.Value.Should().Be(apiResponse);
        }
    }
}
