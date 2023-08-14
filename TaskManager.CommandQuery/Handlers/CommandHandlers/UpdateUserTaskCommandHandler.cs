using MediatR;
using TaskManager.Core.Commands;
using TaskManager.Domain.DTOs;
using TaskManager.Service.Interfaces;

namespace TaskManager.Core.Handlers.CommandHandlers
{
    /// <summary>
    /// Handler responsible for processing the UpdateUserTaskCommand.
    /// </summary>
    public class UpdateUserTaskCommandHandler : IRequestHandler<UpdateUserTaskCommand, UserTaskResponseDTO>
    {
        private readonly IUserTaskService _service;

        public UpdateUserTaskCommandHandler(IUserTaskService service)
        {
            this._service = service;
        }

        public async Task<UserTaskResponseDTO> Handle(UpdateUserTaskCommand command, CancellationToken cancellationToken)
        {
            return await this._service.UpdateTask(command.UserTask, command.Id);
        }
    }
}