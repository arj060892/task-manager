using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TaskManager.Core.Commands;
using TaskManager.Core.Queries;
using TaskManager.Domain.DTOs;

namespace TaskManager.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserTasksController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<UserTasksController> _logger;

        public UserTasksController(IMediator mediator, ILogger<UserTasksController> logger)
        {
            this._mediator = mediator;
            this._logger = logger;
        }

        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(IEnumerable<UserTaskResponseDTO>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Type = typeof(string))]
        public async Task<ActionResult<IEnumerable<UserTaskResponseDTO>>> GetAllUserTasks()
        {
            this._logger.LogInformation("Fetching all user tasks.");
            var query = new GetAllUserTasksQuery();
            var tasks = await this._mediator.Send(query);
            this._logger.LogInformation($"Fetched {tasks?.Count()} user tasks.");
            return this.Ok(tasks);
        }

        [HttpGet("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(UserTaskResponseDTO))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(string))]
        public async Task<ActionResult<UserTaskResponseDTO>> GetUserTask(int id)
        {
            if (id <= 0)
            {
                this._logger.LogWarning($"Invalid task id: {id} provided.");
                return this.BadRequest("Invalid task id.");
            }

            this._logger.LogInformation($"Fetching user task with id: {id}.");
            var query = new GetUserTaskByIdQuery(id);
            var task = await this._mediator.Send(query);
            if (task == null)
            {
                this._logger.LogWarning($"No task found for id: {id}.");
                return this.NotFound($"No task found for id: {id}.");
            }
            this._logger.LogInformation($"Fetched task details for id: {id}.");
            return this.Ok(task);
        }

        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.Created, Type = typeof(UserTaskResponseDTO))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(string))]
        public async Task<ActionResult<UserTaskResponseDTO>> CreateUserTask(UserTaskRequestDTO userTask)
        {
            var command = new CreateUserTaskCommand() { UserTask = userTask };

            if (command == null || !this.ModelState.IsValid)
            {
                this._logger.LogWarning("Invalid task data provided.");
                return this.BadRequest(this.ModelState);
            }

            this._logger.LogInformation("Creating new user task.");
            var result = await this._mediator.Send(command);
            this._logger.LogInformation($"User task with id: {result.Id} created successfully.");
            return this.CreatedAtAction(nameof(GetUserTask), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(UserTaskResponseDTO))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdateUserTask(int id, UpdateUserTaskCommand command)
        {
            if (id <= 0)
            {
                this._logger.LogWarning($"Invalid task id: {id} provided for update.");
                return this.BadRequest("Invalid task id.");
            }

            if (command == null || !this.ModelState.IsValid)
            {
                this._logger.LogWarning("Invalid task data provided for update.");
                return this.BadRequest(this.ModelState);
            }

            command.Id = id;
            this._logger.LogInformation($"Updating user task with id: {id}.");
            var result = await this._mediator.Send(command);
            if (result == null)
            {
                this._logger.LogWarning($"No task found for id: {id} for update.");
                return this.NotFound($"No task found for id: {id}.");
            }
            this._logger.LogInformation($"User task with id: {id} updated successfully.");
            return this.Ok(result);
        }

        [HttpDelete("{id}")]
        [SwaggerResponse((int)HttpStatusCode.NoContent)]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(string))]
        public async Task<IActionResult> DeleteUserTask(int id)
        {
            if (id <= 0)
            {
                this._logger.LogWarning($"Invalid task id: {id} provided for deletion.");
                return this.BadRequest("Invalid task id.");
            }

            this._logger.LogInformation($"Deleting user task with id: {id}.");
            var command = new DeleteUserTaskCommand { Id = id };
            var success = await this._mediator.Send(command);
            if (success)
            {
                this._logger.LogInformation($"User task with id: {id} deleted successfully.");
                return this.NoContent();
            }
            this._logger.LogWarning($"No task found for id: {id} for deletion.");
            return this.NotFound($"No task found for id: {id}.");
        }
    }
}