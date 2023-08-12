using MediatR;
using TaskManager.Core.Commands;
using TaskManager.Domain.DTOs;
using TaskManager.Service.Interfaces;

namespace TaskManager.Core.Handlers.CommandHandlers
{
    /// <summary>
    /// Handler responsible for processing the CreateUserTaskCommand.
    /// </summary>
    public class CreateUserTaskCommandHandler : IRequestHandler<CreateUserTaskCommand, UserTaskResponseDTO>
    {
        private readonly IUserTaskService _service;

        public CreateUserTaskCommandHandler(IUserTaskService service)
        {
            this._service = service;
        }

        public async Task<UserTaskResponseDTO> Handle(CreateUserTaskCommand command, CancellationToken cancellationToken)
        {
            return await this._service.AddTaskAsync(command.UserTask);
        }
    }
}
