using MediatR;
using TaskManager.Core.Commands;
using TaskManager.Service.Interfaces;

namespace TaskManager.Core.Handlers.CommandHandlers
{
    /// <summary>
    /// Handler responsible for processing the DeleteUserTaskCommand.
    /// </summary>
    public class DeleteUserTaskCommandHandler : IRequestHandler<DeleteUserTaskCommand, bool>
    {
        private readonly IUserTaskService _service;

        public DeleteUserTaskCommandHandler(IUserTaskService service)
        {
            this._service = service;
        }

        public async Task<bool> Handle(DeleteUserTaskCommand command, CancellationToken cancellationToken)
        {
            var userTask = await this._service.GetTaskByIdAsync(command.Id);
            if (userTask != null)
            {
                return await this._service.DeleteTask(command.Id);
            }
            return false;
        }
    }
}