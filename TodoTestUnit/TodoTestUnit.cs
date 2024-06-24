using Repository;
using Repository.Interfaces;
using TodoList.Controllers;
using Moq;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.FeatureManagement;
using Castle.Core.Logging;
using Microsoft.Extensions.Logging;

namespace TodoTestUnit
{
    public class TodoTestUnit
    {


        //ONLY TESTS FOR GET ENDPOINT
        private readonly TodosController _controller;
        private readonly Mock<IRepository> _mockTodoRepository;
        private readonly Mock<IFeatureManager> _mockfeatureManager;
        private readonly Mock<ILogger<TodosController>> _loggerMock;


        public TodoTestUnit()
        {

            _mockTodoRepository = new Mock<IRepository>();
            _mockfeatureManager = new Mock<IFeatureManager>();
            _loggerMock = new Mock<ILogger<TodosController>>();
            _controller = new TodosController(_mockTodoRepository.Object, _mockfeatureManager.Object, _loggerMock.Object);
        }


        [Fact]
        public async Task Get_FeatureEnabled_ReturnsOk()
        {
            // Arrange
            _mockfeatureManager.Setup(fm => fm.IsEnabledAsync("FeatureGet")).ReturnsAsync(true);

            // Act
            var result = await _controller.Get();

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Get_FeatureDisabled_ReturnsNotFound()
        {
            // Arrange
            _mockfeatureManager.Setup(fm => fm.IsEnabledAsync("FeatureGet")).ReturnsAsync(false);

            // Act
            var result = await _controller.Get();

            // Assert
            Assert.IsType<NotFoundObjectResult>(result); // Verifica si se devuelve un NotFound
        }


        [Fact]
        public async Task Get_ReturnsOkResult_WhenTodosExist()
        {

            // Arrange
            _mockfeatureManager.Setup(fm => fm.IsEnabledAsync("FeatureGet")).ReturnsAsync(true);
            var todos = new List<Todo>
            {
                new Todo { Id = 1, Description = "make breakfast" },
                new Todo { Id = 2, Description = "sleep" }
            };
            _mockTodoRepository.Setup(repo => repo.Get()).Returns(todos);

            // Act
            var result = await _controller.Get();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedTodos = Assert.IsType<List<Todo>>(okResult.Value);
            Assert.Equal(todos.Count, returnedTodos.Count);
        }


        [Fact]
        public async Task Get_ReturnsBadRequest_WhenModelStateIsInvalid()
        {
            // Arrange
            _mockfeatureManager.Setup(fm => fm.IsEnabledAsync("FeatureGet")).ReturnsAsync(true);
            _controller.ModelState.AddModelError("Key", "An error occurs");

            // Act
            var result = await _controller.Get();

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }



    }
}