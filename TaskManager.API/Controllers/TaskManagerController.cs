using MediatR;
using Microsoft.AspNetCore.Mvc;
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

        public UserTasksController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserTaskResponseDTO>>> GetAllUserTasks()
        {
            var query = new GetAllUserTasksQuery();
            throw new Exception("Somethign wrong");
            var tasks = await this._mediator.Send(query);
            return this.Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserTaskResponseDTO>> GetUserTask(int id)
        {
            var query = new GetUserTaskByIdQuery(id);
            var task = await this._mediator.Send(query);
            if (task == null)
            {
                return this.NotFound();
            }
            return this.Ok(task);
        }

        // POST: api/UserTasks
        [HttpPost]
        public async Task<ActionResult<UserTaskResponseDTO>> CreateUserTask(CreateUserTaskCommand command)
        {
            var result = await this._mediator.Send(command);
            return this.CreatedAtAction(nameof(GetUserTask), new { id = result.Id }, result);
        }

        // PUT: api/UserTasks/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserTask(int id, UpdateUserTaskCommand command)
        {
            command.Id = id;
            var result = await this._mediator.Send(command);
            if (result == null)
            {
                return this.NotFound();
            }
            return this.Ok(result);
        }

        // DELETE: api/UserTasks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserTask(int id)
        {
            var command = new DeleteUserTaskCommand { Id = id };
            var success = await this._mediator.Send(command);
            if (success)
            {
                return this.NoContent();
            }
            return this.NotFound();
        }
    }
}