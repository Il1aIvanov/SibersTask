using BusinessLogic.DTOs;
using BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebApi.Controllers;

namespace SibersTaskTest;

public class EmployeeControllerTests
{
    private readonly Mock<IEmployeeService> _mockService;
    private readonly EmployeeController _controller;

    public EmployeeControllerTests()
    {
        _mockService = new Mock<IEmployeeService>();
        _controller = new EmployeeController(_mockService.Object);
    }

    [Fact]
    public async Task GetEmployeeAsync_ShouldReturnOk_WhenEmployeeExists()
    {
        _mockService.Setup(s => s.GetByIdAsync(1, default)).ReturnsAsync(new EmployeeDto { Id = 1 });
        var result = await _controller.GetEmployeeAsync(1);
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.IsType<EmployeeDto>(okResult.Value);
    }

    [Fact]
    public async Task GetEmployeeAsync_ShouldReturnNotFound_WhenNotFound()
    {
        _mockService.Setup(s => s.GetByIdAsync(1, default)).ReturnsAsync((EmployeeDto?)null);
        var result = await _controller.GetEmployeeAsync(1);
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task DeleteEmployeeAsync_ShouldReturnNoContent()
    {
        var result = await _controller.DeleteEmployeeAsync(1);
        Assert.IsType<NoContentResult>(result);
    }
    
    [Fact]
        public async Task GetAllEmployeesAsync_ReturnsOk()
        {
            // Arrange
            _mockService.Setup(s => s.GetAllAsync(default)).ReturnsAsync([]);

            // Act
            var result = await _controller.GetAllEmployeesAsync();

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task GetEmployeeAsync_Found_ReturnsOk()
        {
            // Arrange
            var dto = new EmployeeDto { Id = 1 };
            _mockService.Setup(s => s.GetByIdAsync(1, It.IsAny<CancellationToken>())).ReturnsAsync(dto);

            // Act
            var result = await _controller.GetEmployeeAsync(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(dto, okResult.Value);
        }

        [Fact]
        public async Task GetEmployeeAsync_NotFound_ReturnsNotFound()
        {
            // Arrange
            _mockService.Setup(s => s.GetByIdAsync(2, It.IsAny<CancellationToken>())).ReturnsAsync((EmployeeDto?)null);

            // Act
            var result = await _controller.GetEmployeeAsync(2);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task CreateEmployeeAsync_Valid_ReturnsNoContent()
        {
            // Arrange
            var dto = new EmployeeCreateDto();

            // Act
            var result = await _controller.CreateEmployeeAsync(dto);

            // Assert
            _mockService.Verify(s => s.CreateAsync(dto, It.IsAny<CancellationToken>()), Times.Once);
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task UpdateEmployeeAsync_IdMismatch_ReturnsBadRequest()
        {
            // Arrange
            var dto = new EmployeeUpdateDto { Id = 10 };

            // Act
            var result = await _controller.UpdateEmployeeAsync(5, dto);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task UpdateEmployeeAsync_Valid_ReturnsNoContent()
        {
            // Arrange
            var dto = new EmployeeUpdateDto { Id = 1 };

            // Act
            var result = await _controller.UpdateEmployeeAsync(1, dto);

            // Assert
            _mockService.Verify(s => s.UpdateAsync(dto, It.IsAny<CancellationToken>()), Times.Once);
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteEmployeeAsync_Valid_ReturnsNoContent()
        {
            // Arrange
            var id = 1;

            // Act
            var result = await _controller.DeleteEmployeeAsync(id);

            // Assert
            _mockService.Verify(s => s.DeleteAsync(id, It.IsAny<CancellationToken>()), Times.Once);
            Assert.IsType<NoContentResult>(result);
        }
}