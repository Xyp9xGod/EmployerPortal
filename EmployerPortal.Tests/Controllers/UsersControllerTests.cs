using EmployerPortal.Controllers;
using EmployerPortal.Interfaces;
using EmployerPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace EmployerPortal.Tests.Controllers;

public class UsersControllerTests
{
    private readonly Mock<IUserService> _userServiceMock = new();
    private readonly Mock<ILogger<UsersController>> _loggerMock = new();
    private readonly UsersController _controller;

    public UsersControllerTests()
    {
        _controller = new UsersController(_loggerMock.Object, _userServiceMock.Object);
    }

    [Fact]
    public async Task Welcome_WithMissingUsername_ReturnsBadRequest()
    {
        // Act
        var result = await _controller.Welcome(null);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Username is required.", badRequestResult.Value.GetType().GetProperty("Message").GetValue(badRequestResult.Value));
    }

    [Fact]
    public async Task Welcome_WithExistingUser_ReturnsGreeting()
    {
        // Arrange
        var testUser = new User { Username = "testuser", Name = "Test User" };
        _userServiceMock.Setup(x => x.GetUserByUsernameAsync("testuser"))
            .ReturnsAsync(testUser);

        // Act
        var result = await _controller.Welcome("testuser");

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal($"Hello, {testUser.Name}", okResult.Value.GetType().GetProperty("Message").GetValue(okResult.Value));
    }

    [Fact]
    public async Task Welcome_WithNonExistentUser_ReturnsNotFound()
    {
        // Arrange
        _userServiceMock.Setup(x => x.GetUserByUsernameAsync("unknown"))
            .ReturnsAsync((User)null);

        // Act
        var result = await _controller.Welcome("unknown");

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Equal("User not found.", notFoundResult.Value.GetType().GetProperty("Message").GetValue(notFoundResult.Value));
    }

    [Fact]
    public async Task Welcome_WhenServiceThrowsException_ReturnsInternalServerError()
    {
        // Arrange
        _userServiceMock.Setup(x => x.GetUserByUsernameAsync("erroruser"))
            .ThrowsAsync(new Exception("Database error"));

        // Act
        var result = await _controller.Welcome("erroruser");

        // Assert
        var statusCodeResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(500, statusCodeResult.StatusCode);
        Assert.Equal("An error occurred while processing your request.", statusCodeResult.Value.GetType().GetProperty("Message").GetValue(statusCodeResult.Value));
    }
}