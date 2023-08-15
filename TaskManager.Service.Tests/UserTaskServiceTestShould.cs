using AutoMapper;
using Moq;
using NUnit.Framework;
using TaskManager.Data.Entities;
using TaskManager.Domain.DTOs;
using TaskManager.Repository.Interfaces;
using TaskManager.Service.Implementations;

namespace TaskManager.Service.Tests
{
    [TestFixture]
    public class UserTaskServiceTestShould
    {
        private UserTaskService _userTaskService;
        private Mock<IUserTaskRepository> _userRepositoryMock;
        private Mock<IMapper> _mapperMock;

        [SetUp]
        public void Setup()
        {
            this._userRepositoryMock = new Mock<IUserTaskRepository>();
            this._mapperMock = new Mock<IMapper>();
            this._userTaskService = new UserTaskService(this._userRepositoryMock.Object, this._mapperMock.Object);
        }

        [Test]
        public async Task GetAllTasksAsync_ReturnsListOfTasks()
        {
            // Arrange
            var tasksFromRepository = new List<UserTask>
            {
                new UserTask { Id = 1, Title = "Task 1" },
                new UserTask { Id = 2, Title = "Task 2" }
            };
            this._userRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(tasksFromRepository);

            // Act
            var result = await this._userTaskService.GetAllTasksAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IEnumerable<UserTaskResponseDTO>>(result);
        }

        [Test]
        public async Task GetTaskByIdAsync_WithValidId_ReturnsTask()
        {
            // Arrange
            var taskId = 1;
            var taskFromRepository = new UserTask { Id = taskId, Title = "Task 1" };

            this._userRepositoryMock.Setup(repo => repo.GetByIdAsync(taskId)).ReturnsAsync(taskFromRepository);

            var expectedMappedResult = new UserTaskResponseDTO { Id = taskId, Title = "Task 1" };
            this._mapperMock.Setup(mapper => mapper.Map<UserTaskResponseDTO>(taskFromRepository)).Returns(expectedMappedResult);

            // Act
            var result = await this._userTaskService.GetTaskByIdAsync(taskId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedMappedResult, result);
        }



        [Test]
        public async Task AddTaskAsync_ValidData_ReturnsAddedTask()
        {
            // Arrange
            var userTaskRequestDTO = new UserTaskRequestDTO { Title = "New Task" };
            var addedUserTask = new UserTask { Id = 1, Title = "New Task" };
            this._mapperMock.Setup(mapper => mapper.Map<UserTask>(userTaskRequestDTO)).Returns(addedUserTask);
            this._userRepositoryMock.Setup(repo => repo.AddAsync(addedUserTask)).ReturnsAsync(addedUserTask);

            var expectedMappedResult = new UserTaskResponseDTO { Id = 1, Title = "New Task" };
            this._mapperMock.Setup(mapper => mapper.Map<UserTaskResponseDTO>(addedUserTask)).Returns(expectedMappedResult);

            // Act
            var result = await this._userTaskService.AddTaskAsync(userTaskRequestDTO);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedMappedResult, result);
        }

        [Test]
        public async Task UpdateTask_ValidData_ReturnsUpdatedTask()
        {
            // Arrange
            var taskId = 1;
            var userTaskRequestDTO = new UserTaskRequestDTO { Title = "Updated Task" };
            var updatedUserTask = new UserTask { Id = taskId, Title = "Updated Task" };
            this._mapperMock.Setup(mapper => mapper.Map<UserTask>(userTaskRequestDTO)).Returns(updatedUserTask);
            this._userRepositoryMock.Setup(repo => repo.UpdateAsync(updatedUserTask)).ReturnsAsync(updatedUserTask);

            var expectedMappedResult = new UserTaskResponseDTO { Id = taskId, Title = "Updated Task" };
            this._mapperMock.Setup(mapper => mapper.Map<UserTaskResponseDTO>(updatedUserTask)).Returns(expectedMappedResult);

            // Act
            var result = await this._userTaskService.UpdateTask(userTaskRequestDTO, taskId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedMappedResult, result);
        }


        [Test]
        public async Task DeleteTask_ValidId_ReturnsTrue()
        {
            // Arrange
            var taskId = 1;
            var taskFromRepository = new UserTask { Id = taskId, Title = "Task 1" };
            this._userRepositoryMock.Setup(repo => repo.GetByIdAsync(taskId)).ReturnsAsync(taskFromRepository);
            this._userRepositoryMock.Setup(repo => repo.DeleteAsync(taskFromRepository)).ReturnsAsync(true);

            // Act
            var result = await this._userTaskService.DeleteTask(taskId);

            // Assert
            Assert.IsTrue(result);
        }


    }
}
