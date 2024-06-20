using Repository;
using Repository.Interfaces;
using TodoList.Controllers;
using Moq;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.FeatureManagement;

namespace TodoTestUnit
{
    public class TodoTestUnit
    {


        //ONLY TESTS FOR GET ENDPOINT
        private readonly TodosController _controller;
        private readonly Mock<IRepositoryTodo> _mockTodoRepository;
        private readonly Mock<IFeatureManager> _mockfeatureManager;

        public TodoTestUnit()
        {

            _mockTodoRepository = new Mock<IRepositoryTodo>();
            _mockfeatureManager = new Mock<IFeatureManager>();
            _controller = new TodosController(_mockTodoRepository.Object, _mockfeatureManager.Object);
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
            // Arrange
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
            _controller.ModelState.AddModelError("Key", "An error occurs");

            // Act
            var result = await _controller.Get();

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }



    }
}