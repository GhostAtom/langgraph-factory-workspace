using Xunit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Threading.Tasks;

public class BookFunctionsTests
{
    [Fact]
    public async Task TestAddBook_Success()
    {
        // Arrange
        var request = new Mock<HttpRequest>();
        var logger = Mock.Of<ILogger>();

        // Act
        var response = await AddBookFunction.Run(request.Object, logger);

        // Assert
        var result = Assert.IsType<OkObjectResult>(response);
        Assert.Equal("Book added successfully", result.Value);
    }

    [Fact]
    public async Task TestGetBook_Success()
    {
        // Arrange
        var logger = Mock.Of<ILogger>();

        // Act
        var response = await GetBookFunction.Run(new DefaultHttpContext().Request, "1", logger);

        // Assert
        var result = Assert.IsType<OkObjectResult>(response);
    }

    // Additional tests for UpdateBook, DeleteBook, etc.
}