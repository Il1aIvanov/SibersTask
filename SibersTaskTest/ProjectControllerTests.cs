using BusinessLogic.DTOs;
using BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebApi.Controllers;

namespace SibersTaskTest;

public class ProjectControllerTests
{
    private readonly Mock<IProjectService> _mockService;
    private readonly ProjectController _controller;

    public ProjectControllerTests()
    {
        _mockService = new Mock<IProjectService>();
        _controller = new ProjectController(_mockService.Object);
    }

    [Fact]
    public async Task GetProjectAsync_WhenProjectExists_ReturnsOk()
    {
        // Arrange
        var project = new ProjectDto { Id = 1 };
        _mockService.Setup(s => s.GetByIdAsync(1, It.IsAny<CancellationToken>())).ReturnsAsync(project);

        // Act
        var result = await _controller.GetProjectAsync(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(project, okResult.Value);
    }

    [Fact]
    public async Task GetProjectAsync_WhenProjectDoesNotExist_ReturnsNotFound()
    {
        // Arrange
        _mockService.Setup(s => s.GetByIdAsync(99, It.IsAny<CancellationToken>())).ReturnsAsync((ProjectDto?)null);

        // Act
        var result = await _controller.GetProjectAsync(99);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task CreateProjectAsync_InvalidModelState_ReturnsBadRequest()
    {
        // Arrange
        _controller.ModelState.AddModelError("ProjectName", "Required");

        // Act
        var result = await _controller.CreateProjectAsync(new ProjectCreateDto());

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task UpdateProjectAsync_IdMismatch_ReturnsBadRequest()
    {
        // Arrange
        var dto = new ProjectUpdateDto { Id = 2 };

        // Act
        var result = await _controller.UpdateProjectAsync(1, dto);

        // Assert
        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public async Task DeleteProjectAsync_CallsService_ReturnsNoContent()
    {
        // Act
        var result = await _controller.DeleteProjectAsync(1, CancellationToken.None);

        // Assert
        _mockService.Verify(s => s.DeleteAsync(1, CancellationToken.None), Times.Once);
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task FilterProjectsAsync_ValidRequest_ReturnsOk()
    {
        // Arrange
        var filter = new ProjectFilterDto { MinPriority = 1, MaxPriority = 5 };
        _mockService.Setup(s => s.FilterAsync(filter, It.IsAny<CancellationToken>())).ReturnsAsync(new List<ProjectDto>());

        // Act
        var result = await _controller.FilterProjectsAsync(filter, CancellationToken.None);

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task AddEmployeeAsync_CallsService_ReturnsNoContent()
    {
        // Act
        var result = await _controller.AddEmployeeAsync(1, 2, CancellationToken.None);

        // Assert
        _mockService.Verify(s => s.AddEmployeeAsync(1, 2, CancellationToken.None), Times.Once);
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task RemoveEmployeeAsync_CallsService_ReturnsNoContent()
    {
        // Act
        var result = await _controller.RemoveEmployeeAsync(1, 2, CancellationToken.None);

        // Assert
        _mockService.Verify(s => s.RemoveEmployeeAsync(1, 2, CancellationToken.None), Times.Once);
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task GetProjectAsync_ServiceThrowsException_ReturnsStatus500()
    {
        // Arrange
        _mockService.Setup(s => s.GetByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>())).ThrowsAsync(new Exception("Unexpected"));

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _controller.GetProjectAsync(1));
    }
}