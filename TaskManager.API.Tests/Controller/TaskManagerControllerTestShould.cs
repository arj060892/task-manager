using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using TaskManager.API.Controllers;
using TaskManager.Core.Commands;
using TaskManager.Core.Queries;
using TaskManager.Domain.DTOs;

namespace TaskManager.API.Tests
{
    [TestFixture]
    public class UserTasksControllerTestShould
    {
        private UserTasksController _controller;
        private Mock<IMediator> _mediatorMock;
        private Mock<ILogger<UserTasksController>> _loggerMock;

        [SetUp]
        public void Setup()
        {
            this._mediatorMock = new Mock<IMediator>();
            this._loggerMock = new Mock<ILogger<UserTasksController>>();
            this._controller = new UserTasksController(this._mediatorMock.Object, this._loggerMock.Object);
        }

        [Test]
        public async Task GetAllUserTasks_ReturnsOkResultWithTasks()
        {
            // Arrange
            var expectedTasks = new List<UserTaskResponseDTO>
            {
                new UserTaskResponseDTO { Id = 1, Title = "Task 1" },
                new UserTaskResponseDTO { Id = 2, Title = "Task 2" }
            };
            this._mediatorMock.Setup(m => m.Send(It.IsAny<GetAllUserTasksQuery>(), default)).ReturnsAsync(expectedTasks);

            // Act
            var result = await this._controller.GetAllUserTasks();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = (OkObjectResult)result.Result;
            var tasks = (IEnumerable<UserTaskResponseDTO>)okResult.Value;
            CollectionAssert.AreEquivalent(expectedTasks, tasks);
        }

        [Test]
        public async Task GetUserTask_WithValidId_ReturnsOkResult()
        {
            // Arrange
            var taskId = 1;
            var userTask = new UserTaskResponseDTO { Id = taskId, Title = "Task 1" };
            this._mediatorMock.Setup(m => m.Send(It.IsAny<GetUserTaskByIdQuery>(), default)).ReturnsAsync(userTask);

            // Act
            var result = await this._controller.GetUserTask(taskId);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = (OkObjectResult)result.Result;
            var retrievedTask = (UserTaskResponseDTO)okResult.Value;
            Assert.AreEqual(taskId, retrievedTask.Id);
        }

        [Test]
        public async Task GetUserTask_WithInvalidId_ReturnsBadRequest()
        {
            // Arrange
            var invalidId = -1;

            // Act
            var result = await this._controller.GetUserTask(invalidId);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }

        [Test]
        public async Task CreateUserTask_InvalidModelState_ReturnsBadRequest()
        {
            // Arrange
            var userTask = new UserTaskRequestDTO();
            this._controller.ModelState.AddModelError("Title", "Title is required");

            // Act
            var result = await this._controller.CreateUserTask(userTask);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }

        // ... Previous setup and test methods ...

        [Test]
        public async Task UpdateUserTask_WithValidIdAndData_ReturnsOkResult()
        {
            // Arrange
            var taskId = 1;
            var userTaskRequest = new UserTaskRequestDTO { Title = "Updated Task" };
            var updatedUserTask = new UserTaskResponseDTO { Id = taskId, Title = "Updated Task" };
            this._mediatorMock.Setup(m => m.Send(It.IsAny<UpdateUserTaskCommand>(), default)).ReturnsAsync(updatedUserTask);

            // Act
            var result = await this._controller.UpdateUserTask(taskId, userTaskRequest);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            var resultTask = (UserTaskResponseDTO)okResult.Value;
            Assert.AreEqual(taskId, resultTask.Id);
            Assert.AreEqual(userTaskRequest.Title, resultTask.Title);
        }

        [Test]
        public async Task UpdateUserTask_WithInvalidId_ReturnsBadRequest()
        {
            // Arrange
            var invalidId = -1;
            var userTaskRequest = new UserTaskRequestDTO { Title = "Updated Task" };

            // Act
            var result = await this._controller.UpdateUserTask(invalidId, userTaskRequest);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public async Task UpdateUserTask_WithInvalidModelState_ReturnsBadRequest()
        {
            // Arrange
            var taskId = 1;
            var userTaskRequest = new UserTaskRequestDTO();
            this._controller.ModelState.AddModelError("Title", "Title is required");

            // Act
            var result = await this._controller.UpdateUserTask(taskId, userTaskRequest);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public async Task DeleteUserTask_WithValidId_ReturnsNoContentResult()
        {
            // Arrange
            var taskId = 1;
            this._mediatorMock.Setup(m => m.Send(It.IsAny<DeleteUserTaskCommand>(), default)).ReturnsAsync(true);

            // Act
            var result = await this._controller.DeleteUserTask(taskId);

            // Assert
            Assert.IsInstanceOf<NoContentResult>(result);
        }

        [Test]
        public async Task DeleteUserTask_WithInvalidId_ReturnsBadRequest()
        {
            // Arrange
            var invalidId = -1;

            // Act
            var result = await this._controller.DeleteUserTask(invalidId);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public async Task DeleteUserTask_TaskNotFound_ReturnsNotFoundResult()
        {
            // Arrange
            var taskId = 1;
            this._mediatorMock.Setup(m => m.Send(It.IsAny<DeleteUserTaskCommand>(), default)).ReturnsAsync(false);

            // Act
            var result = await this._controller.DeleteUserTask(taskId);

            // Assert
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }
    }
}